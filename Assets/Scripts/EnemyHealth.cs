using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;

    public void ProcessHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            print("Enemy is dead");
            // Play deathSFX?
            // Play death animation?
            Destroy(gameObject);
        }
    }
}
