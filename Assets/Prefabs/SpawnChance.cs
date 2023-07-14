using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPrefab
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float spawnChance;
}

public class SpawnChance : MonoBehaviour
{
    public SpawnPrefab[] spawnPrefabs; // Array of prefabs and their spawn chances

    void Start()
    {
        foreach (SpawnPrefab spawnPrefab in spawnPrefabs)
        {
            float randomValue = Random.value;
            if (randomValue < spawnPrefab.spawnChance)
            {
                Instantiate(spawnPrefab.prefab, transform.position, Quaternion.identity);
            }
        }
    }
}
