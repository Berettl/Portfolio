using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public GameObject foodPrefab;

    public Text scoreText; // Reference to UI Text component for displaying score

    private List<Transform> snakeParts = new List<Transform>();
    private Vector2 direction;
    private float moveTimer;
    public float moveInterval = 0.2f;

    public Vector2Int gridSize = new Vector2Int(10, 10); // Set grid size for wall collision
    public Color gridColor = Color.green; // Color for the grid lines

    private int score = 0; // Score variable

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        HandleInput();
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            MoveSnake();
            moveTimer = 0;
        }
    }

    void StartGame()
    {
        direction = Vector2.right;
        GameObject head = Instantiate(snakeHeadPrefab, Vector3.zero, Quaternion.identity);
        snakeParts.Add(head.transform);
        FoodManager.SpawnFood(foodPrefab); // Correctly call SpawnFood with the foodPrefab
        UpdateScoreText(); // Update the UI score when starting
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            direction = Vector2.right;
    }

    void MoveSnake()
    {
        Vector3 newHeadPosition = snakeParts[0].position + (Vector3)direction;

        // Check for wall collision
        if (Mathf.Abs(newHeadPosition.x) > gridSize.x / 2 || Mathf.Abs(newHeadPosition.y) > gridSize.y / 2)
        {
            GameOver();
            return;
        }

        // Check for self-collision
        foreach (Transform part in snakeParts)
        {
            if (part.position == newHeadPosition)
            {
                GameOver();
                return;
            }
        }

        // Move the head to the new position
        GameObject newHead = Instantiate(snakeHeadPrefab, newHeadPosition, Quaternion.identity);
        snakeParts.Insert(0, newHead.transform);

        // Change the previous head into a body part
        if (snakeParts.Count > 1)
        {
            Transform oldHead = snakeParts[1];
            GameObject newBodyPart = Instantiate(snakeBodyPrefab, oldHead.position, Quaternion.identity);
            Destroy(oldHead.gameObject); // Replace the old head
            snakeParts[1] = newBodyPart.transform; // Add body part to the snake list
        }

        // Check for food collision
        if (Vector3.Distance(FoodManager.foodPosition, newHeadPosition) < 0.5f) // Adjust tolerance as needed
        {
            FoodManager.DestroyFood(); // Destroy the food when eaten
            FoodManager.SpawnFood(foodPrefab); // Spawn new food
            IncreaseScore(); // Increase the score when food is eaten
        }
        else
        {
            // Remove the last body part if no food is eaten
            Destroy(snakeParts[snakeParts.Count - 1].gameObject);
            snakeParts.RemoveAt(snakeParts.Count - 1);
        }
    }

    void IncreaseScore()
    {
        score++; // Increase score by 1
        UpdateScoreText(); // Update the UI text
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the score displayed on the UI
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Additional game-over logic, like restarting the game, showing a UI, etc.
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        // Draw vertical grid lines
        for (int x = -gridSize.x / 2; x <= gridSize.x / 2; x++)
        {
            Gizmos.DrawLine(new Vector3(x, -gridSize.y / 2, 0), new Vector3(x, gridSize.y / 2, 0));
        }

        // Draw horizontal grid lines
        for (int y = -gridSize.y / 2; y <= gridSize.y / 2; y++)
        {
            Gizmos.DrawLine(new Vector3(-gridSize.x / 2, y, 0), new Vector3(gridSize.x / 2, y, 0));
        }

        // Draw the outer boundary
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(gridSize.x, gridSize.y, 0));
    }
}
