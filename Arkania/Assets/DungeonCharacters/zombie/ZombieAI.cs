using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public GameObject Player;
    Animator animator;
    private float timer = 0.0f;
    private string currentState;
    private string previousState;
    private AnimatorStateInfo stateInfo;
    NavMeshAgent agent;

    public float walkSpeed = 1.0f;
    public float attackDistance = 10.0f;
    public float attackDamage = 10.0f;
    public float attackDelay = 5.0f;
    public float hp = 20.0f;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = "";
        attackDistance = 3.2f;
        attackDelay = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemySight>().CanSee)
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);
            if (distance > attackDistance)
            {
                agent.isStopped = false;
                AnimationSet("run");
                agent.SetDestination(Player.transform.position);
            }
            else
            {
                agent.isStopped = true;
                if (timer <= 0)
                {
                    AnimationSet("attack");
                    //Player.SendMessage("takeHit", attackDamage);
                    timer = attackDelay;
                }
            }
            if (timer > 0) timer -= Time.deltaTime;
        }
    }

    private void AnimationSet(string animationToPlay)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimationReset();
        if (currentState == "")
        {
            currentState = animationToPlay;
            /*
            if (stateInfo.IsName("Base Layer.run") && currentState != "run")
            {
                animator.SetBool("runToIdle", true);
            }
            */
            if (currentState == "attack" && previousState != "attack") animator.SetBool("runToIdle", true);
            if (currentState == "run" && previousState != "run") animator.SetBool("attackToIdle", true);
            string state = "idleTo" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
            animator.SetBool(state, true);
            previousState = currentState;
            currentState = "";
        }
    }

    private void AnimationReset()
    {
        animator.SetBool("idleToRun", false);
        animator.SetBool("idleToAttack", false);
        animator.SetBool("idleToDie", false);
        animator.SetBool("runToIdle", false);
        animator.SetBool("attackToIdle", false);
    }
}
