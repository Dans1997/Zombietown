﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;

    [Tooltip("Time in seconds to destroy zombie after death")]
    [SerializeField] float deathDelay = 5f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip damageSFX;

    public void ProcessHit(float damage)
    {
        // Calls OnDamageTaken on every MonoBehaviour in this game object or any of its children
        BroadcastMessage("OnDamageTaken", damage);
    }

    private void OnDamageTaken(float damage)
    {
        health -= damage;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(damageSFX, .8f);

        if (health > 0) return;

        audioSource.Stop();
        audioSource.PlayOneShot(deathSFX, .3f);
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        FindObjectOfType<SpawnerController>()?.DecreaseZombieNumber();

        //Handle Drop
        GameObject drop = GetComponent<DropHandler>()?.GetDrop();
        Vector3 dropPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if(drop) Instantiate(drop, dropPos, Quaternion.identity);

        Destroy(gameObject, deathDelay);
    }
}
