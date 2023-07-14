using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TilePrefab
{
    public GameObject prefab;
    [Range(0f, 100f)]
    public float spawnPercentage;
}

public class WorldGenerator : MonoBehaviour
{
    public int worldWidth = 50;  // Width of the world grid
    public int worldHeight = 50; // Height of the world grid
    public int seed = 0; // Seed for random number generation
    public float noiseScale = 0.1f; // Scale of the Perlin noise

    public TilePrefab[] tilePrefabs; // Array of tile prefabs and spawn percentages

    void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        Random.InitState(seed);

        NormalizeSpawnPercentages();

        for (int y = worldHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                float noiseValue = Mathf.PerlinNoise((float)x * noiseScale, (float)y * noiseScale);

                float cumulativeSpawnPercentage = 0f;
                GameObject selectedPrefab = null;

                foreach (TilePrefab tilePrefab in tilePrefabs)
                {
                    cumulativeSpawnPercentage += tilePrefab.spawnPercentage / 100f;

                    if (noiseValue <= cumulativeSpawnPercentage)
                    {
                        selectedPrefab = tilePrefab.prefab;
                        break;
                    }
                }

                if (selectedPrefab != null)
                {
                    Vector3 position = new Vector3(x, y, 0f);
                    Instantiate(selectedPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    void NormalizeSpawnPercentages()
    {
        float totalSpawnPercentage = 0f;
        foreach (TilePrefab tilePrefab in tilePrefabs)
        {
            totalSpawnPercentage += tilePrefab.spawnPercentage;
        }

        if (totalSpawnPercentage == 100f)
        {
            return;
        }

        float adjustmentFactor = 100f / totalSpawnPercentage;
        foreach (TilePrefab tilePrefab in tilePrefabs)
        {
            tilePrefab.spawnPercentage *= adjustmentFactor;
        }
    }
}