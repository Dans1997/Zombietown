﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;

    float distanceToTarget = Mathf.Infinity;
    public bool isProvoked = false;

    // Visuals in Editor Mode
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= chaseRange)
            isProvoked = true;

        if (isProvoked)
            EngageTarget();
    }

    private void EngageTarget()
    {
        float stoppingDistance = GetComponent<NavMeshAgent>().stoppingDistance;

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
        print("Attacking Target");
        GetComponent<Animator>().SetBool("attack", true);
    }
}
