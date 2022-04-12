using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Tooltip("Player Gameoject to spawn")]
    [SerializeField] GameObject playerObject = default;

    [Tooltip("The position buffer applied when spawning the player.\n(0, 0) means that player will be spawned at the top-left corner of the starting tile")]
    [SerializeField] Vector2 buffer = default;

    private void OnEnable()
    {
        //Listen to the LevelGenerated event to spawn the player when the level is completely generated
        LevelGenerator.ELevelGenerated += SpawnPlayer;
    }

    private void OnDisable()
    {
        LevelGenerator.ELevelGenerated -= SpawnPlayer;
    }

    private void SpawnPlayer(Vector2Int startingCoordinates, Vector2Int endingCoordinates)
    {
        Debug.Log($"Starting Coordinates are : {startingCoordinates}");
        Debug.Log($"Ending Coordinates are : {endingCoordinates}");

        GameObject player = Instantiate<GameObject>(playerObject, new Vector3(startingCoordinates.x * 20 + buffer.x, (startingCoordinates.y * 20 + buffer.y) * -1, 0), Quaternion.identity);

        Debug.Log($"Player was spawned at {player.transform.position.ToString()}");
    }
}
