using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;
    public GameObject obstaclePrefab;
    public GameObject obstacleRoad;
    public GameObject coinPrefab;

    [Range(1, 5)]
    public int minEmptyTiles = 3; // Tambah jarak minimum antara obstacle
    private static int tilesSinceLastObstacle = 0;
    private static bool firstTile = true;

    private void Start()
    {
        groundSpawner = GameObject.FindFirstObjectByType<GroundSpawner>();
        if (groundSpawner == null) Debug.LogError("GroundSpawner tidak ditemukan di scene!");

        SpawnObstacles(); // Spawn obstacle dengan lebih teratur
        SpawnCoins();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && groundSpawner != null)
        {
            groundSpawner.SpawnTile();
            Destroy(gameObject, 2f);
        }
    }

    private void SpawnObstacles()
    {
        if (!ShouldSpawnObstacle()) return;

        // **Pastikan obstacle road dan obstacle biasa muncul dengan jarak**
        if (Random.value > 0.5f)
        {
            SpawnObstacle(obstaclePrefab, new int[] { 2, 3, 4 }, Quaternion.identity); // Obstacle biasa
        }
        else
        {
            SpawnObstacle(obstacleRoad, new int[] { 5, 6, 7 }, Quaternion.identity); // Obstacle road
        }
    }

    private void SpawnObstacle(GameObject obstacle, int[] spawnIndices, Quaternion rotation)
    {
        Transform spawnPoint = transform.GetChild(spawnIndices[Random.Range(0, spawnIndices.Length)]);
        Instantiate(obstacle, spawnPoint.position, rotation, transform);
    }

    private void SpawnCoins()
    {
        Vector3[] lanes = { new Vector3(-5.3f, 1f, 0), new Vector3(0.7f, 1f, 0), new Vector3(6.7f, 1f, 0) };
        Vector3 lanePosition = lanes[Random.Range(0, lanes.Length)];

        int coinCount = Random.Range(3, 7); // Random jumlah coin 3-6 agar tidak terlalu banyak
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 spawnPosition = transform.position + lanePosition + new Vector3(0, 0, i * 2f);
            Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(-90, 0, 0), transform);
        }
    }

    private bool ShouldSpawnObstacle()
    {
        if (firstTile)
        {
            firstTile = false;
            return false;
        }

        tilesSinceLastObstacle++;

        if (tilesSinceLastObstacle >= minEmptyTiles)
        {
            tilesSinceLastObstacle = 0;
            return true;
        }

        return false;
    }
}
