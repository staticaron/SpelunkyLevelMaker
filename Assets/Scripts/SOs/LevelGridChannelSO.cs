using UnityEngine;

[CreateAssetMenu(fileName = "LevelGridChannelSO", menuName = "SpelunkyLevelMaker/LevelGridChannelSO", order = 0)]
public class LevelGridChannelSO : ScriptableObject
{
    public delegate Vector2 GetPositionFromGridCoordinates(Vector2Int coordinates);
    public event GetPositionFromGridCoordinates EGetPositionFromGridCoordinates;

    public delegate void UpdateLevelGrid(Vector2Int coordinates, int value);
    public event UpdateLevelGrid EUpdateLevelGrid;

    public delegate int GetValueAtGridCoordinate(Vector2Int coordinate);
    public event GetValueAtGridCoordinate EGetValueAtGridCoordinate;

    public Vector2 RaiseGetPositionFromGridCoordinates(Vector2Int coordinates)
    {
        if (EGetPositionFromGridCoordinates == null)
        {
            Debug.LogError("No Level Grid exist to handle this function call");
            return Vector2.zero;
        }

        return EGetPositionFromGridCoordinates.Invoke(coordinates);
    }

    public void RaiseUpdateLevelGrid(Vector2Int coordinates, int value)
    {
        EUpdateLevelGrid?.Invoke(coordinates, value);
    }

    public int RaiseGetValueAtGridCoordinate(Vector2Int gridCoordinate)
    {
        if (EGetValueAtGridCoordinate == null)
        {
            Debug.Log("No Level Grid exist to handle this function call");
            return -1;
        }

        return EGetValueAtGridCoordinate.Invoke(gridCoordinate);
    }
}