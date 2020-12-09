using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animat;
    public string parameter_walk = "Walk";
    public string parameter_run = "Run";
    public string trigger_attack = "Attack";
    public string trigger_dead = "Dead";

    // Start is called before the first frame update
    void Awake()
    {
        animat = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Walk(bool walk)
    {
        animat.SetBool(parameter_walk, walk);
    }
    public void Run(bool run)
    {
        animat.SetBool(parameter_run, run);
    }
    public void Attack()
    {
        animat.SetTrigger(trigger_attack);
    }
    public void Dead()
    {
        animat.SetTrigger(trigger_dead);
    }
 
}
