using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] LevelGrid levelGrid;
    [SerializeField] GameObject levelPieceTemplate;

    [ContextMenu("Generate Level")]
    private void GenerateLevel()
    {
        GameObject levelPieceGO = Instantiate<GameObject>(levelPieceTemplate);
        LevelPiece levelPiece = levelPieceGO.GetComponent<LevelPiece>();

        //Place first object    
        levelPiece.PlaceLevelPiece(Vector2Int.one);
    }
}
