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

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float attackDistance = 10.0f;
    public float attackDamage = 10.0f;
    public float attackDelay = 5.0f;
    public float hp = 20.0f;
    bool alive = true;
    GameObject[] locations;
    Vector3 _goal;
    public AudioSource audio;
    bool attack = false;

    // Use this for initialization
    void Start()
    {
        _goal = transform.position;
        locations = GameObject.FindGameObjectsWithTag("lastpoints");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        currentState = "";
        attackDistance = 3.5f;
        attackDelay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        if (GetComponent<EnemySight>().CanSee)
        {
            if(!audio.isPlaying) audio.Play();
            float distance = Vector3.Distance(transform.position, Player.transform.position);
            if (distance > attackDistance)
            {
                agent.speed = runSpeed;
                agent.isStopped = false;
                AnimationSet("run");
                agent.SetDestination(Player.transform.position);
            }
            else
            {
                agent.isStopped = true;
                attack = true;
                if (timer <= 0)
                {
                    AnimationSet("attack");
                    Player.SendMessage("takeHit", attackDamage);
                    timer = attackDelay;
                }
            }
            if (timer > 0) timer -= Time.deltaTime;
        }
        else if(tag == "fogzombie")
        {
            agent.isStopped = false;
            audio.Stop();
            if ((Vector3.Distance(_goal, transform.position) < 10f) || attack)
            {
                attack = false;
                Patrol();
            }
        }
    }

    void Patrol()
    {
        //first choose a location
        GameObject location = locations[Random.RandomGen.Next(locations.Length)];
        agent.speed = walkSpeed;
        agent.isStopped = false;
        AnimationSet("run");
        agent.SetDestination(location.transform.position);
        _goal = location.transform.position;
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
            if(animationToPlay == "die")
            {
                animator.SetBool("attackToIdle", true);
            }
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

    public void TakeHit(float damage)
    {
        //Debug.Log("zombie hit " + damage);
        hp -= damage;
        if(hp <= 0)
        {
            AnimationSet("die");
            alive = false;
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            agent.SetDestination(Player.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player && !GetComponent<EnemySight>().CanSee)
        {
            agent.isStopped = false;
            agent.SetDestination(Player.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        agent.isStopped = false;
        agent.SetDestination(Player.transform.position);
    }
}
