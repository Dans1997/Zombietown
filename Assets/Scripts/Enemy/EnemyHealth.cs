using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip damageSFX;

    float currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    private void OnRecycle() => Start();

    public void ProcessHit(float damage)
    {
        // Calls OnDamageTaken on every MonoBehaviour in this game object or any of its children
        BroadcastMessage("OnDamageTaken", damage);
    }

    private void OnDamageTaken(float damage)
    {
        currentHealth -= damage;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(damageSFX, .8f);

        if (currentHealth > 0) return;

        StartCoroutine(RecycleZombie(audioSource));

        //Handle Drop
        GameObject drop = GetComponent<DropHandler>()?.GetDrop();
        Vector3 dropPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if(drop) Instantiate(drop, dropPos, Quaternion.identity);
    }

    IEnumerator RecycleZombie(AudioSource audioSource)
    {
        Animator animator = GetComponent<Animator>();
        EnemyAI enemyAI = GetComponent<EnemyAI>();
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        CapsuleCollider collider = GetComponent<CapsuleCollider>();

        audioSource.Stop();
        audioSource.PlayOneShot(deathSFX, .3f);
        animator.SetTrigger("die");
        enemyAI.enabled = false;
        navMeshAgent.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(3f);

        animator.Play("Idle", -1, 0f);
        enemyAI.enabled = true;
        navMeshAgent.enabled = true;
        collider.enabled = true;
        ObjectPooler.ObjectPoolerInstance.RecycleObject(ObjectPooler.PoolKey.ZombiePool, gameObject);
    }
}
