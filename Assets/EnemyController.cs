using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_animat;
    private NavMeshAgent nav_mesh_agent;
    private EnemyState enemy_state;
    public float walk_speed = 0.5f;
    public float run_speed = 4f;
    public float chase_distance = 7f;
    private float current_chase_distance;
    public float attack_distance = 1.8f;
    public float chase_after_attack_distance = 2f;
    public float patrol_radius_min = 20f;
    public float patrol_radius_max = 60f;
    public float patrol_for_this_time = 15f;
    private float patrol_timer;
    public float wait_before_attack = 2f;
    private float attack_timer;
    private Transform target;
    public GameObject attack_point;
    //private EnemyAudio enemy_audio;

    void Awake()
    {
        enemy_animat = GetComponent<EnemyAnimator>();
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_state = EnemyState.PATROL;
        patrol_timer = patrol_for_this_time;
        attack_timer = wait_before_attack;
        current_chase_distance = chase_distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_state == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemy_state == EnemyState.CHASE)
        {
            Chase();
        }
        if (enemy_state == EnemyState.ATTACK)
        {
            Attack();
        }
    }
    // Patrol
    void Patrol()
    {
        nav_mesh_agent.isStopped = false;
        nav_mesh_agent.speed = walk_speed;
        patrol_timer += Time.deltaTime;

        if (patrol_timer > patrol_for_this_time)
        {
            SetNewRandomDestination();

            patrol_timer = 0f;
        }

        if (nav_mesh_agent.velocity.sqrMagnitude > 0)
        {
            enemy_animat.Walk(true);
        }
        else
        {
            enemy_animat.Walk(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= chase_distance)
        {

            enemy_animat.Walk(false);
            enemy_state = EnemyState.CHASE;
            //enemy_audio.Play_ScreamSound();
        }
    }

    // Chase
    void Chase()
    {
        // enable the agent to move again
        nav_mesh_agent.isStopped = false;
        nav_mesh_agent.speed = run_speed;

        // set the player's position as the destination
        // because we are chasing(running towards) the player
        nav_mesh_agent.SetDestination(target.position);

        if (nav_mesh_agent.velocity.sqrMagnitude > 0)
        {
            enemy_animat.Run(true);
        }
        else
        {
            enemy_animat.Run(false);
        }

        // if the distance between enemy and player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_distance)
        {

            // stop the animations
            enemy_animat.Run(false);
            enemy_animat.Walk(false);
            enemy_state = EnemyState.ATTACK;

            // reset the chase distance to previous
            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }

        }
        else if (Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            // player run away from enemy

            // stop running
            enemy_animat.Run(false);

            enemy_state = EnemyState.PATROL;
      
            patrol_timer = patrol_for_this_time;

            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }


        }
    }

        // Attack
        void Attack()
    {
        nav_mesh_agent.velocity = Vector3.zero;
        nav_mesh_agent.isStopped = true;

        attack_timer += Time.deltaTime;

        if (attack_timer > wait_before_attack)
        {
            enemy_animat.Attack();
            attack_timer = 0f;
            // play attack sound
            //enemy_audio.Play_AttackSound();
        }

        if (Vector3.Distance(transform.position, target.position) >
           attack_distance + chase_after_attack_distance)
        {

            enemy_state = EnemyState.CHASE;

        }

    }

    void SetNewRandomDestination()
    {

        float rand_Radius = Random.Range(patrol_radius_min, patrol_radius_max);

        Vector3 random_dir = Random.insideUnitSphere * rand_Radius;
        random_dir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(random_dir, out navHit, rand_Radius, -1);

        nav_mesh_agent.SetDestination(navHit.position);

    }

    void Turn_On_AttackPoint()
    {
        attack_point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        get; set;
    }
}
