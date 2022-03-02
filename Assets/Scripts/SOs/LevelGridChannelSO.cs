using UnityEngine;

[CreateAssetMenu(fileName = "LevelGridChannelSO", menuName = "SpelunkyLevelMaker/LevelGridChannelSO", order = 0)]
public class LevelGridChannelSO : ScriptableObject
{
    public delegate Vector2 GetPositionFromGridCoordinates(Vector2Int coordinates);
    public event GetPositionFromGridCoordinates EGetPositionFromGridCoordinates;

    public delegate void UpdateLevelGrid(Vector2Int coordinates, int value);
    public event UpdateLevelGrid EUpdateLevelGrid;

    public Vector2 RaiseGetPositionFromGridCoordinates(Vector2Int coordinates)
    {
        if (EGetPositionFromGridCoordinates == null)
        {
            Debug.LogError("No Level Grid exist to perform this command");
            return Vector2.zero;
        }

        return EGetPositionFromGridCoordinates.Invoke(coordinates);
    }

    public void RaiseUpdateLevelGrid(Vector2Int coordinates, int value)
    {
        EUpdateLevelGrid?.Invoke(coordinates, value);
    }
}