using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] int increaseRateDelay;

    [Header("Spawner")]
    [SerializeField] Canvas enableSpawnerCanvas;
    [SerializeField] AudioClip enableSpawnerSFX;
    [SerializeField] AudioClip enableSpawnerMusic;

    [Header("Timer")]
    [SerializeField] Text timerText;

    int timeElapsed = 0;

    float increaseRate = 1f;
    bool hasEnabledSpawners = false;

    // Cached Components
    Base playerBase;

    // Start is called before the first frame update
    private void Start()
    {
        playerBase = FindObjectOfType<Base>();
        enableSpawnerCanvas.enabled = false;
        timerText.text = (increaseRateDelay - timeElapsed).ToString() + "s";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerHealth>() || hasEnabledSpawners) return;
        hasEnabledSpawners = true;
        StartCoroutine(HandleSpawnRate());
        StartCoroutine(ShowCanvas(enableSpawnerCanvas, 10f));
        BroadcastMessage("SetSpawnerActive", true);

        // SFXs
        FindObjectOfType<MusicPlayer>()?.ChangeClipTo(enableSpawnerMusic, true);
        AudioSource.PlayClipAtPoint(enableSpawnerSFX, Camera.main.transform.position, 1f);
    }

    IEnumerator HandleSpawnRate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            timeElapsed += 1;
            timerText.text = (increaseRateDelay - timeElapsed).ToString() + "s";
            playerBase.HandleBaseActivation(timeElapsed);
            HandleSpawnersRates();
        }
    }

    private void HandleSpawnersRates()
    {
        if (timeElapsed == increaseRateDelay)
        {
            BroadcastMessage("SpeedUpSpawnRateBy", increaseRate);
            timeElapsed = 0;

            // SFXs
            AudioSource.PlayClipAtPoint(enableSpawnerSFX, Camera.main.transform.position, 1f);
        }
    }

    IEnumerator ShowCanvas(Canvas canvas, float canvasTime)
    {
        canvas.enabled = true;
        yield return new WaitForSeconds(canvasTime);
        canvas.enabled = false;
    }

}
