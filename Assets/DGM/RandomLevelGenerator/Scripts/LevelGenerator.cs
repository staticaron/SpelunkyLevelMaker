using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    // Delegates and events
    public delegate void LevelGenerated(Vector2Int endingCoordinates);
    public static event LevelGenerated ELevelGenerated;

    [SerializeField] LevelGrid levelGrid;
    [SerializeField] GameObject levelPieceTemplate;

    [Tooltip("If true, new tile will generate untill no new placement is possible")]
    [SerializeField] bool completeGeneration;

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
        Vector2Int startingCoordinates = GenerateRandomPath();
        PopulateEmptyTiles();

        ELevelGenerated?.Invoke(startingCoordinates);
    }

    private Vector2Int GenerateRandomPath()
    {
        GameObject levelPieceGO = piecePoolerChannelSO.RaiseRequestPoolObjectFromGates(GateType.None, GateType.None);
        LevelPiece levelPiece = levelPieceGO.GetComponent<LevelPiece>();
        levelPieceGO.transform.parent = null;
        levelPieceGO.SetActive(true);
        Vector2Int randomTopTileCoordinates = levelGridChannelSO.RaiseGetRandomTopTile();

        //Place first object and start the cycle
        levelPiece.PlaceLevelPiece(randomTopTileCoordinates, GateType.None, completeGeneration);

        return randomTopTileCoordinates;
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
