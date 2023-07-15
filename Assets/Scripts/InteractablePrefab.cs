using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePrefab : MonoBehaviour
{
    public int healthPool = 3; // Health pool of the prefab
    public int rewardValue = 10; // Reward value when the health reaches zero
    public int damageValue = 1; // Damage value inflicted on interaction

    public void Interact()
    {
        healthPool -= damageValue; // Subtract the damage value from the health pool

        if (healthPool <= 0)
        {
            // Health reached zero, destroy the prefab and add to the reward value
            Destroy(gameObject);
            // Add to the reward value here (e.g., increase the player's score)
        }
        else
        {
            // Health is still above zero, perform other interactions or display remaining health, etc.
            Debug.Log("Prefab interacted! Remaining health: " + healthPool);
        }
    }
}