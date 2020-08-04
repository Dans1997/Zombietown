using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float currentHealth = 10;
    [SerializeField] Text healthText;
    [SerializeField] GameObject gameOverCanvas;

    [Header("Player SFXs")]
    [SerializeField] AudioClip healthRecoverSFX;
    [SerializeField] AudioClip damageSFX;
    [SerializeField] AudioClip loseMusic;

    bool hasTakenDamage = false;
    float health = 10;
    float maxHealth = 10;
    int healthRecoveryDelay = 7;
    int healthRecoveryAmount = 5;

    // Cached Components
    AudioSource audioSource;

    private void Start()
    {
        health = 10;
        audioSource = GetComponent<AudioSource>();
        gameOverCanvas.SetActive(false);
        healthText.text = health.ToString();

        // Handle Position Spawn
        HandlePlayerSpawn();      
    }

    private void HandlePlayerSpawn()
    {
        CheckPoint checkPoint = FindObjectOfType<CheckPoint>();
        Base playerBase = FindObjectOfType<Base>();

        if (!checkPoint.IsCheckPointActive()) transform.position = playerBase.transform.position;
        else transform.position = checkPoint.transform.position;
    }

    public void ProcessHit(float damage)
    {
        health -= damage;
        StopAllCoroutines();
        StartCoroutine(HandleHealthRecovery());

        GetComponent<DamageDisplay>().ShowCanvas();
        audioSource.PlayOneShot(damageSFX, 0.3f);

        if (health <= 0)
        {
            FindObjectOfType<MusicPlayer>().ChangeClipTo(loseMusic, false);
            ActivateGameOverCanvas();
        }

        // Handle UI
        healthText.text = health.ToString();
    }

    private void ActivateGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None; // Activate Cursor
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.visible = true;
    }

    IEnumerator HandleHealthRecovery()
    {
        hasTakenDamage = true;
        yield return new WaitForSeconds(healthRecoveryDelay);
        hasTakenDamage = false;

        float newHealth = health + healthRecoveryAmount;
        if (newHealth < maxHealth)
        {
            maxHealth = newHealth;
            currentHealth = newHealth;
        } 
        else
        {
            currentHealth = Mathf.Min(newHealth, maxHealth);
        }

        audioSource.PlayOneShot(healthRecoverSFX, 1f);
        healthText.text = health.ToString();
    }
}