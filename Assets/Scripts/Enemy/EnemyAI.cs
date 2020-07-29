using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Zombie")]
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] bool startProvoked = false;

    [Header("Zombie SFXs")]
    [SerializeField] AudioClip idleSFX;
    [SerializeField] AudioClip engageSFX;

    Transform target;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    // Cached Components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        isProvoked = startProvoked;
        target = FindObjectOfType<PlayerHealth>().transform;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool previousIsProvoked = isProvoked;
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= chaseRange)
            isProvoked = true;

        if (isProvoked)
        {
            EngageTarget();
            if(!previousIsProvoked) audioSource.PlayOneShot(engageSFX, 0.5f);
        }
    }

    private void EngageTarget()
    {
        float stoppingDistance = GetComponent<NavMeshAgent>().stoppingDistance;

        FaceTarget();

        if (distanceToTarget >= stoppingDistance)
            ChaseTarget();

        if (distanceToTarget < stoppingDistance)
            AttackTarget();
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    // Visuals in Editor Mode
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
