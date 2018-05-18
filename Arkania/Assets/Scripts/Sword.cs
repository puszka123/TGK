using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon {


    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider colisionObject)
    {

        Debug.Log("Hit " + colisionObject);
        if(colisionObject.tag == "enemy")
        {
            colisionObject.GetComponent<Bandit>().takeDamage(1);

        }
    }
    public void Attack()
    {
        Debug.Log("Sword Attack!");
        animator.SetTrigger("Attack"); 
    }

}
