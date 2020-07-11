using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;

    public void ProcessHit(float damage)
    {
        // Calls OnDamageTaken on every MonoBehaviour in this game object or any of its children
        BroadcastMessage("OnDamageTaken", damage);
    }

    private void OnDamageTaken(float damage)
    {
        health -= damage;
        if (health > 0) return;

        print("Enemy is dead");
        // Play deathSFX?
        // Play death animation?
        Destroy(gameObject);
    }
}
