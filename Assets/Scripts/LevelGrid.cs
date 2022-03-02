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
    }

    private void OnDisable()
    {
        levelGridChannelSO.EGetPositionFromGridCoordinates -= GetPositionFromGridCoordinate;
        levelGridChannelSO.EUpdateLevelGrid -= UpdateLevelGrid;
    }

    [ContextMenu("Print Grid")]
    private void PrintGrid()
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            string rowString = "";

            for (int col = 0; col < grid.GetLength(1); col++)
            {
                rowString += grid[row, col].ToString();
            }

            print(rowString);
        }
    }

    public Vector2 GetPositionFromGridCoordinate(Vector2Int coordinates)
    {
        var postition = (Vector2)transform.position + new Vector2(coordinates.x * levelPieceWidth, coordinates.y * levelPieceHeight);
        return postition;
    }

    private void UpdateLevelGrid(Vector2Int coordinates, int value)
    {
        grid[coordinates.x, coordinates.y] = value;
    }
}
