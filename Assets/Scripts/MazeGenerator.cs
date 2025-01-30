using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int width = 15;  // Number of cells in X
    public int height = 15; // Number of cells in Z
    public GameObject wallPrefab;
    public GameObject exitPrefab;
    public GameObject player;
    public Transform mazeParent;

    private int[,] maze;
    private List<Vector2Int> directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1),  // Up
        new Vector2Int(1, 0),  // Right
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0)  // Left
    };

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        maze = new int[width, height];

        // Start from bottom-left corner
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int start = new Vector2Int(0, 0);
        maze[start.x, start.y] = 1;
        stack.Push(start);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                Vector2Int chosen = neighbors[Random.Range(0, neighbors.Count)];
                maze[chosen.x, chosen.y] = 1;
                maze[(chosen.x + current.x) / 2, (chosen.y + current.y) / 2] = 1; // Remove wall
                stack.Push(chosen);
            }
            else
            {
                stack.Pop();
            }
        }

        // Set exit in the top-right corner
        maze[width - 1, height - 1] = 2;
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighbor = cell + dir * 2;
            if (IsInsideMaze(neighbor) && maze[neighbor.x, neighbor.y] == 0)
            {
                maze[neighbor.x - dir.x, neighbor.y - dir.y] = 1; // Remove wall
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    bool IsInsideMaze(Vector2Int cell)
    {
        return cell.x >= 0 && cell.y >= 0 && cell.x < width && cell.y < height;
    }

    void BuildMaze()
    {
        Vector3 exitPosition = Vector3.zero;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0) // Place walls
                {
                    Instantiate(wallPrefab, new Vector3(x, 1.5f, y), Quaternion.identity, mazeParent);
                }
                else if (maze[x, y] == 2) // Place exit
                {
                    exitPosition = new Vector3(x, 1, y);
                }
            }
        }

        // Set player at the bottom-left (entry point)
        player.transform.position = new Vector3(0, 1, 0);

        // Instantiate exit
        Instantiate(exitPrefab, exitPosition, Quaternion.identity);
    }
}
