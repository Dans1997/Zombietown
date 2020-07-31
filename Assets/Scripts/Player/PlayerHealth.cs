using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;
    [SerializeField] GameObject gameOverCanvas;

    [Header("Player SFXs")]
    [SerializeField] AudioClip damageSFX;
    [SerializeField] AudioClip loseMusic;

    // Cached Components
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameOverCanvas.SetActive(false);
        transform.position = FindObjectOfType<Base>().transform.position;
    }

    public void ProcessHit(float damage)
    {
        health -= damage;
        GetComponent<DamageDisplay>().ShowCanvas();
        audioSource.PlayOneShot(damageSFX, 0.3f);

        if (health <= 0)
        {
            // Play deathSFX?
            // Play death animation?
            //Destroy(gameObject);
            FindObjectOfType<MusicPlayer>().ChangeClipTo(loseMusic, false);
            ActivateGameOverCanvas();
        }
    }

    private void ActivateGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
        // Activate Cursor
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.visible = true;
    }
}