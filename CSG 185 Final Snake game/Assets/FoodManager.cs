using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static Vector3 foodPosition;
    public static GameObject foodPrefab;
    private static GameObject currentFood;
    public static Vector2Int gridSize = new Vector2Int(30, 10); // Grid size for snapping food

    public static void SpawnFood(GameObject prefab)
    {
        foodPrefab = prefab;

        // Generate random grid-aligned position
        float x = Random.Range(-gridSize.x / 2, gridSize.x / 2 + 1); // Ensure values align with grid
        float y = Random.Range(-gridSize.y / 2, gridSize.y / 2 + 1);
        foodPosition = new Vector3(x, y, 0); // No need for snapping; values are already aligned

        // Spawn food at the calculated position
        currentFood = Instantiate(foodPrefab, foodPosition, Quaternion.identity);
    }

    public static void DestroyFood()
    {
        if (currentFood != null)
        {
            Destroy(currentFood);
            currentFood = null;
        }
    }

    private static Vector3 GetCameraBounds()
    {
        Camera camera = Camera.main;
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;
        return new Vector3(width, height, 0);
    }
}
