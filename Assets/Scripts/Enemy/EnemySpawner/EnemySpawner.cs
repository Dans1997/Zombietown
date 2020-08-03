using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyPrefab;
    [SerializeField] EnemyHealth enemyPanicPrefab;
    [SerializeField] float spawnRate;

    EnemyHealth selectedPrefab;
    bool isEnabled = false;
    float minSpawnRate = 3f;

    //Cached Components
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        selectedPrefab = enemyPrefab;
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(SpawnEnemy());
    }

    void OnEnable()
    {
        StopAllCoroutines();
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

    private void EnterPanicMode() { selectedPrefab = enemyPanicPrefab; spawnRate = 3f; }

    private void SetSpawnerActive(bool active) => isEnabled = active;

    private void SpeedUpSpawnRateBy(float speedUpTime) => spawnRate = Mathf.Max(spawnRate, minSpawnRate);
}
