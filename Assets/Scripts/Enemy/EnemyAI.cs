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
    float updatePathEveryFrameRange = 20f;
    bool isProvoked = false;
    bool startedUpdating = false;

    // Cached Components
    Animator animator;
    AudioSource audioSource;
    NavMeshAgent navAgent;
    NavMeshPath path;

    public void OnDamageTaken() => isProvoked = true;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        navAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        isProvoked = startProvoked;
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
            FaceTarget();
            bool canZombieAttack = (distanceToTarget <= navAgent.stoppingDistance);
            bool isZombieClose = (distanceToTarget <= updatePathEveryFrameRange);

            // Handle Attack when Zombie is Close Enough
            animator.SetBool("attack", canZombieAttack);

            if (!canZombieAttack && !startedUpdating) StartCoroutine(UpdatePath());
            if (isZombieClose) navAgent.SetDestination(target.position);
            if (!previousIsProvoked)
            {
                audioSource.PlayOneShot(engageSFX, 0.5f);
            }
        }
    }

    IEnumerator UpdatePath()
    {
        startedUpdating = true;
        animator.SetBool("attack", false);
        animator.SetTrigger("move");
        while (true)
        {
            if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
                if (distanceToTarget >= updatePathEveryFrameRange)
                    navAgent.SetPath(path);

            yield return new WaitForSeconds(2.5f);

            /* For Debugging
            for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);*/
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    // Visuals in Editor Mode
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
