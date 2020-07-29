using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] float increaseRateDelay;

    [Header("Enable Spawner SFX")]
    [SerializeField] AudioClip enableSpawnerSFX;

    [Header("Music")]
    [SerializeField] AudioClip enableSpawnerMusic;

    float increaseRate = 1f;
    bool hasEnabledSpawners = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerHealth>() || hasEnabledSpawners) return;
        hasEnabledSpawners = true;
        StartCoroutine(HandleSpawnRate());
        BroadcastMessage("SetSpawnerActive", true);
        AudioSource.PlayClipAtPoint(enableSpawnerSFX, Camera.main.transform.position, 1f);
        FindObjectOfType<MusicPlayer>()?.ChangeClipTo(enableSpawnerMusic, true);
    }

    IEnumerator HandleSpawnRate()
    {
        yield return new WaitForSeconds(increaseRateDelay);
        BroadcastMessage("SpeedUpSpawnRateBy", increaseRate);
        AudioSource.PlayClipAtPoint(enableSpawnerSFX, Camera.main.transform.position, 1f);
    }
}
