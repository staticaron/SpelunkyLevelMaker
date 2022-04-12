using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelGenerator : MonoBehaviour
{
    // Delegates and events
    public delegate void LevelGenerated(Vector2Int startingCoordinates, Vector2Int endingCoordinates);
    public static event LevelGenerated ELevelGenerated;

    [SerializeField] LevelGrid levelGrid;
    [SerializeField] GameObject levelPieceTemplate;

    [Tooltip("If true, new tile will generate untill no new placement is possible")]
    [SerializeField] bool completeGeneration = false;

    [Tooltip("If true, Random Tiles will be populated in the empty spaces of the grid")]
    [SerializeField] bool populateEmptySpace = true;

    [Tooltip("If true, Hudles will generate at the edges of the map")]
    [SerializeField] bool populateHurdles = true;

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
        Vector2Int startingCoordinates = levelGridChannelSO.RaiseGetRandomTopTile();

        Vector2Int endingCoordinates = GenerateRandomPath(startingCoordinates);     // Generate Random Continous Path
        if (populateEmptySpace) PopulateEmptyTiles();                               // Place empty tiles with random tiles
        if (populateHurdles) PopulateHurdles();                                     // Place Surrounding Hurdles

        // Level Generated, do other tasks
        ELevelGenerated?.Invoke(startingCoordinates, endingCoordinates);

    }

    private Vector2Int GenerateRandomPath(Vector2Int startingCoordinate)
    {
        GameObject levelPieceGO = piecePoolerChannelSO.RaiseRequestPoolObjectFromGates(GateType.None, GateType.None);
        LevelPiece levelPiece = levelPieceGO.GetComponent<LevelPiece>();
        levelPieceGO.transform.SetParent(transform);
        levelPieceGO.SetActive(true);

        //Place first object and start the cycle
        return levelPiece.PlaceLevelPiece(startingCoordinate, GateType.None, completeGeneration);
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

    private void PopulateHurdles()
    {
        List<Vector2Int> surroundingTileCoordinates = levelGridChannelSO.RaiseGetSurroundingTiles();

        foreach (Vector2Int coordinate in surroundingTileCoordinates)
        {
            GameObject pooledHurdle = piecePoolerChannelSO.RaiseRequestPooledObjectFromType("Hurdle");
            pooledHurdle.transform.position = levelGridChannelSO.RaiseGetPositionFromGridCoordinates(coordinate);
            pooledHurdle.SetActive(true);
        }
    }
}
