using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;

    [Tooltip("Time in seconds to destroy zombie after death")]
    [SerializeField] float deathDelay = 5f;

    public void ProcessHit(float damage)
    {
        // Calls OnDamageTaken on every MonoBehaviour in this game object or any of its children
        BroadcastMessage("OnDamageTaken", damage);
    }

    private void OnDamageTaken(float damage)
    {
        health -= damage;
        if (health > 0) return;

        // Play deathSFX?
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(DestroyEnemy());
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
