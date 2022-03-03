using UnityEngine;

struct SurroudingData
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public Vector2Int GetRandomDirection()
    {
        Vector2Int direction = Vector2Int.zero;

        while (direction == Vector2Int.zero)
        {
            int rand = Random.Range(0, 3);

            //Debug.Log($"Loop is running with rand = {rand} and directions = {up} {down} {left} {right}");

            if (rand == 0 && down == false)
            {
                direction = new Vector2Int(0, 1);
                break;
            }
            else if (rand == 1 && left == false)
            {
                direction = new Vector2Int(-1, 0);
                break;
            }
            else if (rand == 2 && right == false)
            {
                direction = new Vector2Int(1, 0);
                break;
            }
            else
            {
                Debug.Log("Checking Other possibilities");
                continue;
            }
        }

        return direction;
    }
}

public class LevelPiece : MonoBehaviour
{
    [SerializeField] GameObject levelPieceTemplate;

    [SerializeField] LevelGridChannelSO levelGridChannelSO;

    public void PlaceLevelPiece(Vector2Int coordinates, int debugValue)
    {
        transform.position = levelGridChannelSO.RaiseGetPositionFromGridCoordinates(coordinates);
        levelGridChannelSO.RaiseUpdateLevelGrid(coordinates, 1);

        Debug.Log($"This piece was placed at {coordinates}", gameObject);

        if (debugValue > 4)
        {
            return;
        }

        var childLevelPiece = Instantiate<GameObject>(levelPieceTemplate);
        var levelPiece = childLevelPiece.GetComponent<LevelPiece>();

        SurroudingData surrData = GetSurroundingData(coordinates);
        Vector2Int nextDirection = surrData.GetRandomDirection();

        Debug.Log("NEXT Direction is " + nextDirection.ToString());

        levelPiece.PlaceLevelPiece(new Vector2Int(coordinates.x + nextDirection.x, coordinates.y + nextDirection.y), debugValue + 1);
    }

    private SurroudingData GetSurroundingData(Vector2Int coordinates)
    {
        SurroudingData surroudingData = new SurroudingData();

        surroudingData.up = levelGridChannelSO.RaiseGetValueAtGridCoordinate(coordinates - Vector2Int.up) == 0 ? false : true;
        surroudingData.down = levelGridChannelSO.RaiseGetValueAtGridCoordinate(coordinates - Vector2Int.down) == 0 ? false : true;
        surroudingData.left = levelGridChannelSO.RaiseGetValueAtGridCoordinate(coordinates + Vector2Int.left) == 0 ? false : true;
        surroudingData.right = levelGridChannelSO.RaiseGetValueAtGridCoordinate(coordinates + Vector2Int.right) == 0 ? false : true;

        return surroudingData;
    }
}
