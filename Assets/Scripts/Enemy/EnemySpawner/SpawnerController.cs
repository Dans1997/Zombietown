using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] Canvas enableSpawnerCanvas;
    [SerializeField] AudioClip enableSpawnerSFX;
    [SerializeField] AudioClip enableSpawnerMusic;

    [Header("Timer")]
    [SerializeField] Text timerText;

    int increaseRateDelay = 60; // Each wave is 1 minute long
    int timeElapsed = 0;
    int waveNumber = 0;
    float increaseRate = 1f;
    bool hasEnabledSpawners = false;

    // Cached Components
    Base playerBase;

    // Start is called before the first frame update
    private void Start()
    {
        playerBase = FindObjectOfType<Base>();
        waveNumber = playerBase.GetMaxTime() / increaseRateDelay;
        enableSpawnerCanvas.enabled = false;
        UpdateUI();
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
            playerBase.HandleBaseActivation(timeElapsed);
            HandleSpawnersRates();
            UpdateUI();
        }
    }

    private void HandleSpawnersRates()
    {
        if (timeElapsed == increaseRateDelay)
        {
            BroadcastMessage("SpeedUpSpawnRateBy", increaseRate);
            timeElapsed = 0;
            waveNumber--;
            if (waveNumber == 0)
            {
                StopAllCoroutines();
                timerText.text = "Run....";
            }
            UpdateUI();

            // SFXs
            AudioSource.PlayClipAtPoint(enableSpawnerSFX, Camera.main.transform.position, 1f);
        }
    }

    private void UpdateUI()
    {
        if(increaseRateDelay - timeElapsed < 10) timerText.text = waveNumber + "x 0" + (increaseRateDelay - timeElapsed).ToString() + "s";
        else timerText.text = waveNumber + "x " + (increaseRateDelay - timeElapsed).ToString() + "s";
    }

    IEnumerator ShowCanvas(Canvas canvas, float canvasTime)
    {
        canvas.enabled = true;
        yield return new WaitForSeconds(canvasTime);
        canvas.enabled = false;
    }

}
