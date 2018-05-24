using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerWalk : MonoBehaviour {
    static System.Random random;
    GameObject[] points;
    NavMeshAgent navMeshAgent;
    Animator animator;
    AnimatorStateInfo stateInfo;
    bool _goalAchieved;
    float timeToWait;
    Transform _nextPoint;
    const float destinationReachedTreshold = 2f;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        points = GameObject.FindGameObjectsWithTag("goal");
        random = Random.RandomGen;
        _nextPoint = points[random.Next(points.Length)].transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = _nextPoint.position;
        animator.SetBool("walk", true);
	}
	
	// Update is called once per frame
	void Update () {
		if(!_goalAchieved && CheckDestinationReached())
        {
            _goalAchieved = true;
            animator.SetBool("walk", false);
            animator.SetBool("notWalk", true);
            if (random.Next(2) == 0) timeToWait = -1;
            else timeToWait = random.Next(10);
        }
        
        if (_goalAchieved && timeToWait <=0)
        {
            _nextPoint = points[random.Next(points.Length)].transform;
            navMeshAgent.destination = _nextPoint.position;
            animator.SetBool("notWalk", false);
            animator.SetBool("walk", true);
            _goalAchieved = false;
        }
        else if(_goalAchieved)
        {
            timeToWait -= Time.deltaTime;
        }
	}

    bool CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _nextPoint.position);
        if (distanceToTarget < destinationReachedTreshold)
        {
            return true;
        }
        return false;
    }
}
