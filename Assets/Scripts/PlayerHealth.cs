using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;
    [SerializeField] GameObject gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void ProcessHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            print("Player is dead");
            // Play deathSFX?
            // Play death animation?
            //Destroy(gameObject);
            ActivateGameOverCanvas();
        }
    }

    private void ActivateGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
        // Activate Cursor
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}