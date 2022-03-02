using UnityEngine;

public class LevelPiece : MonoBehaviour
{
    [SerializeField] GameObject levelPieceTemplate;

    [SerializeField] LevelGridChannelSO levelGridChannelSO;

    public void PlaceLevelPiece(Vector2Int coordinates)
    {
        transform.position = levelGridChannelSO.RaiseGetPositionFromGridCoordinates(coordinates);
        levelGridChannelSO.RaiseUpdateLevelGrid(coordinates, 1);
        Debug.Log($"This piece was placed at {coordinates}", gameObject);
    }
}
