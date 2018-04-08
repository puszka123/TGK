using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public GameObject playerObject;
    Transform player;
    NavMeshAgent nav;

    private void Awake()
    {
        player = playerObject.transform;
        nav = GetComponent<NavMeshAgent>();
        
        gameObject.GetComponent<Animator>().SetTrigger("startedWalking");

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(player.position);
	}
}
