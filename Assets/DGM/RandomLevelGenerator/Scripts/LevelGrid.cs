using UnityEngine;
using System.Collections.Generic;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;

    [SerializeField] float levelPieceWidth;
    [SerializeField] float levelPieceHeight;

    [SerializeField] int[,] grid;

    [SerializeField] LevelGridChannelSO levelGridChannelSO;

    private void Awake()
    {
        grid = new int[gridWidth, gridHeight];
    }

    private void OnEnable()
    {
        levelGridChannelSO.EGetPositionFromGridCoordinates += GetPositionFromGridCoordinate;
        levelGridChannelSO.EUpdateLevelGrid += UpdateLevelGrid;
        levelGridChannelSO.EGetValueAtGridCoordinate += GetValueAtCoordinates;
        levelGridChannelSO.EGetEmptyTiles += GetEmptyTiles;
        levelGridChannelSO.EGetRandomTopTile += GetRandomTileFromTop;
    }

    private void OnDisable()
    {
        levelGridChannelSO.EGetPositionFromGridCoordinates -= GetPositionFromGridCoordinate;
        levelGridChannelSO.EUpdateLevelGrid -= UpdateLevelGrid;
        levelGridChannelSO.EGetValueAtGridCoordinate -= GetValueAtCoordinates;
        levelGridChannelSO.EGetEmptyTiles -= GetEmptyTiles;
        levelGridChannelSO.EGetRandomTopTile -= GetRandomTileFromTop;
    }

    [ContextMenu("Print Grid")]
    private void PrintGrid()
    {
        for (int col = 0; col < grid.GetLength(1); col++)
        {
            string rowString = "";

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                rowString += grid[row, col].ToString();
            }

            print(rowString);
        }
    }

    // Returns the world position from the grid coordinate
    public Vector2 GetPositionFromGridCoordinate(Vector2Int coordinates)
    {
        var postition = (Vector2)transform.position + new Vector2(coordinates.x * levelPieceWidth, -1 * coordinates.y * levelPieceHeight);
        return postition;
    }

    // Updates the grid provided the new value and the coordinate
    private void UpdateLevelGrid(Vector2Int coordinates, int value)
    {
        grid[coordinates.x, coordinates.y] = value;
    }

    // Returns the value of the grid at a particular coordinate
    private int GetValueAtCoordinates(Vector2Int coordinates)
    {
        if (coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= gridWidth || coordinates.y >= gridHeight)
        {
            return -1;
        }

        return grid[coordinates.x, coordinates.y];
    }

    // Returns a list of tile coordinates that are still empty
    private List<Vector2Int> GetEmptyTiles()
    {
        List<Vector2Int> emptyTiles = new List<Vector2Int>();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == 0)
                {
                    emptyTiles.Add(new Vector2Int(row, col));
                }
            }
        }

        return emptyTiles;
    }

    private Vector2Int GetRandomTileFromTop()
    {
        return new Vector2Int(Random.Range(0, gridWidth), 0);
    }
}
