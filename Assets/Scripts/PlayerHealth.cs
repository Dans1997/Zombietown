using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;

    public void ProcessHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            print("Player is dead");
            // Play deathSFX?
            // Play death animation?
            //Destroy(gameObject);
        }
    }
}