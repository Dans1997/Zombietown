using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyPrefab;
    [SerializeField] EnemyHealth enemyPanicPrefab;
    [SerializeField] float spawnRate;

    EnemyHealth selectedPrefab;
    [SerializeField] bool panicMode = false;
    bool isEnabled = false;
    float minSpawnRate = 5f;

    //Cached Components
    SpawnerController spawnerController;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        selectedPrefab = enemyPrefab;
        spawnerController = FindObjectOfType<SpawnerController>();
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(SpawnEnemy());
    }

    void OnEnable()
    {
        spawnerController = FindObjectOfType<SpawnerController>();
        StopAllCoroutines();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (!meshRenderer.isVisible || panicMode)
            {
                if ((isEnabled && spawnerController.CanSpawnZombies()))
                {
                    EnemyHealth newZombie = Instantiate(enemyPrefab);
                    newZombie.GetComponent<NavMeshAgent>().Warp(transform.position);
                    spawnerController.IncreaseZombieNumber();
                }
            }
        }
    }

    private void EnterPanicMode() { selectedPrefab = enemyPanicPrefab; spawnRate = minSpawnRate; panicMode = true; isEnabled = true; }

    private void SetSpawnerActive(bool active) => isEnabled = active;

    private void SpeedUpSpawnRateBy(float speedUpTime) => spawnRate = Mathf.Max(spawnRate, minSpawnRate);
}
