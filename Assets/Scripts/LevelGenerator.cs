using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] LevelGrid levelGrid;
    [SerializeField] GameObject levelPieceTemplate;

    //SOs
    [SerializeField] PiecePoolerChannelSO piecePoolerChannelSO;
    [SerializeField] LevelGridChannelSO levelGridChannelSO;

    private void Start()
    {
        GenerateLevel();
    }

    [ContextMenu("Generate Level")]
    private void GenerateLevel()
    {
        GenerateRandomPath();
        PopulateEmptyTiles();
    }

    private void GenerateRandomPath()
    {
        GameObject levelPieceGO = piecePoolerChannelSO.RaiseRequestPoolObjectFromGates(GateType.None, GateType.None);
        LevelPiece levelPiece = levelPieceGO.GetComponent<LevelPiece>();
        levelPieceGO.transform.parent = null;
        levelPieceGO.SetActive(true);

        //Place first object and start the cycle
        levelPiece.PlaceLevelPiece(Vector2Int.zero, 0, GateType.LEFT);
    }

    private void PopulateEmptyTiles()
    {
        List<Vector2Int> emptyTileLocations = levelGridChannelSO.RaiseGetEmptyTiles();

        foreach (Vector2Int coordinate in emptyTileLocations)
        {
            GameObject randLevelPiece = piecePoolerChannelSO.RaiseGetRandomPoolObject();
            randLevelPiece.transform.position = levelGridChannelSO.RaiseGetPositionFromGridCoordinates(coordinate);
            randLevelPiece.SetActive(true);
        }
    }
}
