using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void Start()
    {
        // Get the generated world's size
        int worldWidth = FindObjectOfType<WorldGenerator>().worldWidth;
        int worldHeight = FindObjectOfType<WorldGenerator>().worldHeight;

        // Calculate the position to place the player in the middle of the world
        float playerX = worldWidth / 2f;
        float playerY = worldHeight / 2f;

        // Set the player's position
        transform.position = new Vector3(playerX, playerY, 0f);
    }
}
