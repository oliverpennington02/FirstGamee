using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPrefab
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float spawnChance;
    public float maxOffset = 1f;
}

public class SpawnChance : MonoBehaviour
{
    public SpawnPrefab[] spawnPrefabs; // Array of prefabs and their spawn chances

    public float sortingOrderMultiplier = 100f;

    void Start()
    {
        foreach (SpawnPrefab spawnPrefab in spawnPrefabs)
        {
            float randomValue = Random.value;
            if (randomValue < spawnPrefab.spawnChance)
            {
                Vector3 offset = new Vector3(Random.Range(-spawnPrefab.maxOffset, spawnPrefab.maxOffset), Random.Range(-spawnPrefab.maxOffset, spawnPrefab.maxOffset), 0f);
                GameObject instantiatedPrefab = Instantiate(spawnPrefab.prefab, transform.position + offset, Quaternion.identity);
                SetSortingOrder(instantiatedPrefab);
            }
        }
    }

    void SetSortingOrder(GameObject obj)
    {
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * sortingOrderMultiplier);
        }
    }
}