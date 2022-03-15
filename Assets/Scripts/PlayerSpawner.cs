using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerObject = default;

    [SerializeField] Vector2 buffer = default;
    [SerializeField] CameraController cameraController;

    private void OnEnable()
    {
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
        cameraController.target = player.transform;
    }
}
