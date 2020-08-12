using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    private static ObjectPooler _instance;

    public static ObjectPooler ObjectPoolerInstance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null!");
            }

            return _instance;
        }
    
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    public enum PoolKey { ZombiePool }

    [System.Serializable]
    public class Pool
    {
        public PoolKey poolKey;
        public GameObject prefab;
        public int poolSize;
    }

    public Dictionary<PoolKey, Queue<GameObject>> poolDictionary = new Dictionary<PoolKey, Queue<GameObject>>();

    // Pool List
    [SerializeField] public List<Pool> pools;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = gameObject.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.poolKey, objectPool);
        }
    }

    public void RecycleObject(PoolKey key, GameObject recycledObject)
    {
        recycledObject.SetActive(false);
        poolDictionary[key].Enqueue(recycledObject);
    }

    public GameObject SpawnFromPool(PoolKey key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning("Pool " + key + "does not exist!");
            return null;
        }

        if(poolDictionary[key].Count >= 1)
        {
            GameObject objectToSpawn = poolDictionary[key].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.SendMessage("OnRecycle");
            return objectToSpawn;
        }

        return null;
    }

}
