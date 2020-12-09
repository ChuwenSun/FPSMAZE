using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool is_Player, is_Cannibal;

    private bool is_Dead;

    private void Awake()
    {
        if (is_Cannibal)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (is_Player)
        {

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;

        health -= damage;

        if (is_Player)
        {

        }

        if (is_Cannibal)
        {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_distance = 50f;
            }
        }
        if (health <= 0f)
        {
            PlayerDied();
        }
    }
    void PlayerDied()
    {
        if (is_Cannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 1f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

        }
        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < enemies.Length;i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<move>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

            if(tag == "Plyaer")
            {
                Invoke("RestartGame", 3f);
            }
            else
            {
                Invoke("TurnOffGameObject", 3f);
            }
        }
    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
