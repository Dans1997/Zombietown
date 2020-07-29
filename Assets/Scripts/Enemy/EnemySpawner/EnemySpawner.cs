using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyPrefab;
    [SerializeField] float spawnRate;

    bool isEnabled = false;
    float minSpawnRate = 3f;

    //Cached Components
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (isEnabled && !meshRenderer.isVisible) Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void SetSpawnerActive(bool active) { isEnabled = active; }

    private void SpeedUpSpawnRateBy(float speedUpTime)
    {
        spawnRate -= speedUpTime;
        if (spawnRate <= minSpawnRate) spawnRate = 3;
    }
}
