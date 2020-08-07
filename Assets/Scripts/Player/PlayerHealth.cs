using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 12;
    [SerializeField] Text healthText;
    [SerializeField] GameObject gameOverCanvas;

    [Header("Player SFXs")]
    [SerializeField] AudioClip healthRecoverSFX;
    [SerializeField] AudioClip damageSFX;
    [SerializeField] AudioClip loseMusic;

    bool hasTakenDamage = false;
    float currentHealth;
    int healthRecoveryDelay = 7;
    int healthRecoveryAmount = 5;

    // Cached Components
    AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        gameOverCanvas.SetActive(false);
        healthText.text = currentHealth.ToString();

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
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        StopAllCoroutines();
        StartCoroutine(HandleHealthRecovery());

        GetComponent<DamageDisplay>().ShowCanvas();
        audioSource.PlayOneShot(damageSFX, 0.3f);

        if (currentHealth <= 0)
        {
            FindObjectOfType<MusicPlayer>().ChangeClipTo(loseMusic, false);
            ActivateGameOverCanvas();
        }

        // Handle UI
        healthText.text = currentHealth.ToString();
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

        float newHealth = currentHealth + healthRecoveryAmount;
        if (newHealth < maxHealth)
        {
            maxHealth = newHealth;
            currentHealth = newHealth;
        } 
        else
        {
            currentHealth = maxHealth;
        }

        audioSource.PlayOneShot(healthRecoverSFX, 1f);
        healthText.text = currentHealth.ToString();
    }
}