using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;

    [SerializeField] bool panicMode = false;
    bool isEnabled = false;
    float minSpawnRate = 5f;

    //Cached Components
    SpawnerController spawnerController;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
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
                if (isEnabled)
                {
                    GameObject newZombie = ObjectPooler.ObjectPoolerInstance.SpawnFromPool(ObjectPooler.PoolKey.ZombiePool);

                    if(newZombie)
                    {
                        newZombie.GetComponent<NavMeshAgent>().Warp(transform.position);
                    }
                    else
                    {
                        Debug.Log("Can't Spawn" );
                    }
                }
            }
        }
    }

    private void EnterPanicMode() { spawnRate = minSpawnRate; panicMode = true; isEnabled = true; }

    private void SetSpawnerActive(bool active) => isEnabled = active;

    private void SpeedUpSpawnRateBy(float speedUpTime) => spawnRate = Mathf.Max(spawnRate, minSpawnRate);
}
