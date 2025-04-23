using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food")) // Check if the collided object is tagged as Food
        {
            FoodManager.DestroyFood(); // Destroy the food
            FoodManager.SpawnFood(FoodManager.foodPrefab); // Spawn new food
            Destroy(other.gameObject); // Optional: Destroy the food object itself
        }
    }
}
