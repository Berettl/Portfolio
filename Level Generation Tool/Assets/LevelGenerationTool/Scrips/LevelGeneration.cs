using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int gridSize = 20; // Size of the grid for room placement
    public int fixedRoomWidth = 10; // Fixed room width
    public int fixedRoomLength = 10; // Fixed room length
    public int fixedRoomHeight = 10; // Fixed room height
    public GameObject[] wallPrefabs; // Array of wall prefabs to choose from
    public GameObject[] floorPrefabs; // Array of floor prefabs to choose from
    public GameObject[] ceilingPrefabs; // Array of ceiling prefabs to choose from
    public GameObject doorPrefab; // Prefab for doors
    public int roomCount; // Total number of rooms to generate

    public GameObject playerPrefab; // Prefab for the player
    public GameObject enemyPrefab; // Prefab for enemies
    private List<Room> rooms = new List<Room>(); // List to store generated rooms

    private void Start()
    {
        Generate(); // Start the level generation process
    }

    public void Generate()
    {
        rooms.Clear(); // Clear any existing rooms

        // Create the first room at a random position within the grid
        Vector3 firstRoomPosition = new Vector3(Random.Range(0, gridSize - fixedRoomWidth), 0, Random.Range(0, gridSize - fixedRoomLength));
        Room firstRoom = CreateFixedRoom(firstRoomPosition);

        if (firstRoom != null)
        {
            rooms.Add(firstRoom); // Add the first room to the list
            CreateRoom(firstRoom); // Instantiate the first room in the scene
        }

        // Create the remaining rooms
        for (int i = 1; i < roomCount; i++)
        {
            if (!TryPlaceConnectedRoom()) // Attempt to place a connected room
            {
                Debug.LogWarning("Could not place room due to limited space or overlap."); // Log a warning if placement fails
            }
        }

        // Ensure all rooms are connected
        EnsureAllRoomsConnected();

        // Spawn the player and enemies in the generated rooms
        SpawnPlayerInRandomRoom();
        SpawnEnemiesInRooms();
    }

    private Room CreateFixedRoom(Vector3 position)
    {
        // Create a new room with fixed dimensions at the specified position
        return new Room(fixedRoomWidth, fixedRoomLength, position, fixedRoomHeight);
    }

    private bool TryPlaceConnectedRoom()
    {
        int attempts = 0; // Track the number of placement attempts
        while (attempts < 10) // Limit attempts to 10
        {
            Room existingRoom = rooms[Random.Range(0, rooms.Count)]; // Select a random existing room
            Vector3 newPosition = GetAdjacentPosition(existingRoom); // Get a position adjacent to the existing room
            Room newRoom = CreateFixedRoom(newPosition); // Create a new room at the adjacent position

            if (IsRoomPositionValid(newRoom)) // Check if the new room's position is valid
            {
                CreateRoom(newRoom); // Instantiate the new room
                rooms.Add(newRoom); // Add the new room to the list
                MarkSharedWalls(existingRoom, newRoom); // Mark shared walls between the two rooms
                return true; // Successfully placed a connected room
            }
            attempts++; // Increment the attempt counter
        }
        return false; // Failed to place a connected room after 10 attempts
    }

    private Vector3 GetAdjacentPosition(Room existingRoom)
    {
        int side = Random.Range(0, 4); // Randomly select a side to place the new room
        Vector3 newPosition = existingRoom.position; // Start with the existing room's position

        switch (side)
        {
            case 0: newPosition.x -= existingRoom.width; break; // Left side
            case 1: newPosition.x += existingRoom.width; break; // Right side
            case 2: newPosition.z += existingRoom.length; break; // Above
            case 3: newPosition.z -= existingRoom.length; break; // Below
        }

        return newPosition; // Return the new position for the adjacent room
    }

    private bool IsRoomPositionValid(Room newRoom)
    {
        // Check if the new room overlaps with any existing rooms
        foreach (Room room in rooms)
        {
            if (RoomsOverlap(newRoom, room))
            {
                return false; // Position is invalid if it overlaps
            }
        }
        return true; // Position is valid if no overlaps are found
    }

    private bool RoomsOverlap(Room roomA, Room roomB)
    {
        // Check if two rooms overlap based on their positions and dimensions
        return !(roomA.position.x + roomA.width <= roomB.position.x ||
                 roomA.position.x >= roomB.position.x + roomB.width ||
                 roomA.position.z + roomA.length <= roomB.position.z ||
                 roomA.position.z >= roomB.position.z + roomB.length);
    }

    private void CreateRoom(Room room)
    {
        // Instantiate walls for the room
        InstantiateWallAt(room, WallSide.West);
        InstantiateWallAt(room, WallSide.East);
        InstantiateWallAt(room, WallSide.North);
        InstantiateWallAt(room, WallSide.South);

        // Instantiate the floor using a random prefab from the array
        GameObject floorPrefab = floorPrefabs[Random.Range(0, floorPrefabs.Length)];
        GameObject floor = Instantiate(floorPrefab, new Vector3(room.position.x + room.width / 2.0f, 0, room.position.z + room.length / 2.0f), Quaternion.identity);
        floor.transform.localScale = new Vector3(room.width, 1, room.length); // Scale the floor to fit the room
        floor.transform.parent = transform; // Set the parent to the current object

        // Instantiate the ceiling using a random prefab from the array
        GameObject ceilingPrefab = ceilingPrefabs[Random.Range(0, ceilingPrefabs.Length)];
        GameObject ceiling = Instantiate(ceilingPrefab, new Vector3(room.position.x + room.width / 2.0f, room.height, room.position.z + room.length / 2.0f), Quaternion.identity);
        ceiling.transform.localScale = new Vector3(room.width, 1, room.length); // Scale the ceiling to fit the room
        ceiling.transform.parent = transform; // Set the parent to the current object
    }

    private void InstantiateWallAt(Room room, WallSide side)
    {
        Vector3 position = room.position; // Start with the room's position
        Vector3 scale = new Vector3(1, room.height, room.length); // Default scale for walls
        Quaternion rotation = Quaternion.identity; // Default rotation

        GameObject wall = null; // Variable to hold the wall prefab

        switch (side)
        {
            case WallSide.West:
                position += new Vector3(0, room.height / 2.0f, room.length / 2.0f); // Position for the west wall
                break;
            case WallSide.East:
                position += new Vector3(room.width, room.height / 2.0f, room.length / 2.0f); // Position for the east wall
                break;
            case WallSide.North:
                position += new Vector3(room.width / 2.0f, room.height / 2.0f, room.length); // Position for the north wall
                scale = new Vector3(room.width, room.height, 1); // Adjust scale for north wall
                break;
            case WallSide.South:
                position += new Vector3(room.width / 2.0f, room.height / 2.0f, 0); // Position for the south wall
                scale = new Vector3(room.width, room.height, 1); // Adjust scale for south wall
                break;
        }

        // Check if a wall already exists at this position
        GameObject existingWall = GameObject.FindGameObjectsWithTag("Wall")
            .FirstOrDefault(wallObj => Vector3.Distance(wallObj.transform.position, position) < 2f);

        if (existingWall != null)
        {
            // If a wall is close to another wall, destroy the existing wall and replace it with a door
            Destroy(existingWall); // Destroy the existing wall

            // Instantiate a door at the same position, with the same scale and rotation as the destroyed wall
            wall = Instantiate(doorPrefab, position, rotation);
            wall.transform.localScale = scale; // Set the scale for the door
            wall.transform.parent = transform; // Set the parent to the current object
            wall.tag = "Door"; // Mark as door
            Debug.Log("Wall replaced with door at: " + position); // Log the replacement
        }
        else
        {
            // If no nearby wall is found, instantiate a regular wall
            wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], position, rotation);
            wall.transform.localScale = scale; // Set the scale for the wall
            wall.transform.parent = transform; // Set the parent to the current object
            wall.tag = "Wall"; // Mark as wall
        }
    }

    private void MarkSharedWalls(Room roomA, Room roomB)
    {
        // Check and add shared walls between roomA and roomB
        if (roomA.position.x + roomA.width == roomB.position.x) // roomA is to the left of roomB
        {
            Vector3 sharedPosition = new Vector3(roomA.position.x + roomA.width, roomA.height / 2.0f, roomA.position.z + roomA.length / 2.0f);
            roomA.sharedWalls.Add(sharedPosition); // Add shared wall position to roomA
            roomB.sharedWalls.Add(sharedPosition); // Add shared wall position to roomB

            Debug.Log("Shared wall (West side) added: " + sharedPosition); // Log the addition of the shared wall
        }
        else if (roomA.position.x == roomB.position.x + roomB.width) // roomA is to the right of roomB
        {
            Vector3 sharedPosition = new Vector3(roomB.position.x + roomB.width, roomB.height / 2.0f, roomB.position.z + roomB.length / 2.0f);
            roomA.sharedWalls.Add(sharedPosition); // Add shared wall position to roomA
            roomB.sharedWalls.Add(sharedPosition); // Add shared wall position to roomB

            Debug.Log("Shared wall (East side) added: " + sharedPosition); // Log the addition of the shared wall
        }
        else if (roomA.position.z + roomA.length == roomB.position.z) // roomA is below roomB
        {
            Vector3 sharedPosition = new Vector3(roomA.position.x + roomA.width / 2.0f, roomA.height / 2.0f, roomA.position.z + roomA.length);
            roomA.sharedWalls.Add(sharedPosition); // Add shared wall position to roomA
            roomB.sharedWalls.Add(sharedPosition); // Add shared wall position to roomB

            Debug.Log("Shared wall (South side) added: " + sharedPosition); // Log the addition of the shared wall
        }
        else if (roomA.position.z == roomB.position.z + roomB.length) // roomA is above roomB
        {
            Vector3 sharedPosition = new Vector3(roomB.position.x + roomB.width / 2.0f, roomB.height / 2.0f, roomB.position.z + roomB.length);
            roomA.sharedWalls.Add(sharedPosition); // Add shared wall position to roomA
            roomB.sharedWalls.Add(sharedPosition); // Add shared wall position to roomB

            Debug.Log("Shared wall (North side) added: " + sharedPosition); // Log the addition of the shared wall
        }
    }

    private void EnsureAllRoomsConnected()
    {
        // Connect any rooms that aren't already connected by walls
        foreach (Room roomA in rooms)
        {
            foreach (Room roomB in rooms)
            {
                if (roomA != roomB && !AreRoomsConnected(roomA, roomB)) // Check if rooms are not the same and not connected
                {
                    Vector3 connectionPosition = GetConnectionPosition(roomA, roomB); // Get the position for the connection
                    if (connectionPosition != Vector3.zero) // Ensure a valid connection position
                    {
                        // Check if a door already exists at the connection position
                        GameObject existingDoor = GameObject.FindGameObjectsWithTag("Door")
                            .FirstOrDefault(door => Vector3.Distance(door.transform.position, connectionPosition) < 2f);

                        if (existingDoor == null) // If no existing door is found
                        {
                            Debug.Log("Connecting rooms at: " + connectionPosition); // Log the connection
                            // Create a door to connect rooms
                            //Instantiate(doorPrefab, connectionPosition, Quaternion.identity); // Uncomment to instantiate the door
                        }
                    }
                }
            }
        }
    }

    private Vector3 GetConnectionPosition(Room roomA, Room roomB)
    {
        // Check and return the position where a door can be placed
        if (roomA.position.x + roomA.width == roomB.position.x) // roomA is to the left of roomB
        {
            return new Vector3(roomA.position.x + roomA.width, roomA.height / 2.0f, roomA.position.z + roomA.length / 2.0f); // Return position for door
        }
        else if (roomA.position.x == roomB.position.x + roomB.width) // roomA is to the right of roomB
        {
            return new Vector3(roomB.position.x + roomB.width, roomB.height / 2.0f, roomB.position.z + roomB.length / 2.0f); // Return position for door
        }
        else if (roomA.position.z + roomA.length == roomB.position.z) // roomA is below room B
        {
            return new Vector3(roomA.position.x + roomA.width / 2.0f, roomA.height / 2.0f, roomA.position.z + roomA.length); // Return position for door
        }
        else if (roomA.position.z == roomB.position.z + roomB.length) // roomA is above roomB
        {
            return new Vector3(roomB.position.x + roomB.width / 2.0f, roomB.height / 2.0f, roomB.position.z + roomB.length); // Return position for door
        }
        return Vector3.zero; // No valid connection position
    }

    private bool AreRoomsConnected(Room roomA, Room roomB)
    {
        // Check if two rooms are connected by shared walls
        return roomA.sharedWalls.Intersect(roomB.sharedWalls).Any(); // Return true if there are shared walls
    }

    private void SpawnPlayerInRandomRoom()
    {
        // Select a random room and spawn the player in its center
        Room randomRoom = rooms[Random.Range(0, rooms.Count)];
        Instantiate(playerPrefab, randomRoom.position + new Vector3(randomRoom.width / 2, 1, randomRoom.length / 2), Quaternion.identity);
    }

    private void SpawnEnemiesInRooms()
    {
        // Spawn a random number of enemies in each room
        foreach (Room room in rooms)
        {
            int enemyCount = Random.Range(1, 5); // Random number of enemies per room
            for (int i = 0; i < enemyCount; i++)
            {
                // Randomly position enemies within the room
                Vector3 enemyPosition = room.position + new Vector3(Random.Range(0, room.width), 2, Random.Range(0, room.length));
                Instantiate(enemyPrefab, enemyPosition, Quaternion.identity); // Instantiate enemy
            }
        }
    }

    private enum WallSide { West, East, North, South } // Enum for wall sides
}