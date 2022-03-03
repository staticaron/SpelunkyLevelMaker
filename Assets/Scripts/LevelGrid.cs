using UnityEngine;

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
    }

    private void OnDisable()
    {
        levelGridChannelSO.EGetPositionFromGridCoordinates -= GetPositionFromGridCoordinate;
        levelGridChannelSO.EUpdateLevelGrid -= UpdateLevelGrid;
        levelGridChannelSO.EGetValueAtGridCoordinate -= GetValueAtCoordinates;
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

    public Vector2 GetPositionFromGridCoordinate(Vector2Int coordinates)
    {
        var postition = (Vector2)transform.position + new Vector2(coordinates.x * levelPieceWidth, -1 * coordinates.y * levelPieceHeight);
        return postition;
    }

    private void UpdateLevelGrid(Vector2Int coordinates, int value)
    {
        grid[coordinates.x, coordinates.y] = value;
    }

    private int GetValueAtCoordinates(Vector2Int coordinates)
    {
        if (coordinates.x < 0 || coordinates.y < 0 || coordinates.x >= gridWidth || coordinates.y >= gridHeight)
        {
            return -1;
        }

        return grid[coordinates.x, coordinates.y];
    }
}
