using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour {

    public int maxHealth = 5;
    public int currentHealt = 5;
    void Start()
    {
        currentHealt = maxHealth;
    }

    public void takeDamage( int amount)
    {
        
        currentHealt -= amount;
        if(currentHealt <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }
}
