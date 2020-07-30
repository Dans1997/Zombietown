using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    [System.Serializable]
    private class Drop
    {
        public GameObject dropPrefab;
        [Range(0, 1)] public float dropChance;
    }

    [SerializeField] Drop[] drops;

    public GameObject GetDrop()
    {
        float range = 0;
        for (var i = 0; i < drops.Length; i++)
            range += drops[i].dropChance;

        if (range > 1f)
        {
            Debug.LogError("Drop Rates sum up to more than 100%.");
            return null;
        }

        float rand = Random.Range(0f, 1f);
        float min = 0;

        for (var i = 0; i < drops.Length; i++)
        {
            if (rand > min && rand < min + drops[i].dropChance)
            {
                return drops[i].dropPrefab;
            }
            min += drops[i].dropChance;
        }
        return null;
    }
}
