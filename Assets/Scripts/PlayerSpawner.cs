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

    private void SpawnPlayer(Vector2Int endingCoordinates)
    {
        GameObject player = Instantiate<GameObject>(playerObject, new Vector3(endingCoordinates.x * 20 + buffer.x, (endingCoordinates.y * 20 + buffer.y) * -1, 0), Quaternion.identity);
        cameraController.target = player.transform;
    }
}
