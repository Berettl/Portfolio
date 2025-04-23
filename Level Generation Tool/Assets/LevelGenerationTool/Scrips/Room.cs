using UnityEngine;
using System.Collections.Generic;  

public class Room 
{
    public int width;      // Width of the room (X dimension)
    public int length;     // Length of the room (Z dimension)
    public Vector3 position; // Position of the room in the 3D space
    public int height;     // Fixed height of the room (Y dimension)
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject ceilingPrefab;

    // Stores the positions of walls that should be doors when shared with adjacent rooms
    public HashSet<Vector3> sharedWalls;

    public Room(int width, int length, Vector3 position, int height)
    {
        this.width = width;
        this.length = length;
        this.position = position;
        this.height = height; // Set the fixed height for the room
        this.sharedWalls = new HashSet<Vector3>(); // Initialize shared wall positions
    }
}
