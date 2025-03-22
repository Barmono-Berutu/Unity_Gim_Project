using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    [Range(1, 5)]
    public int minEmptyTiles = 2; // Minimal 2 tile kosong sebelum obstacle muncul lagi

    private static int tilesSinceLastObstacle = 0;
    private static bool firstTile = true;

    private void Start()
    {
        groundSpawner = GameObject.FindAnyObjectByType<GroundSpawner>();

        if (groundSpawner == null)
        {
            Debug.LogError("GroundSpawner tidak ditemukan di scene!");
        }

        SpawnObstacle();
        SpawnCoins();
    }

    private void OnTriggerExit(Collider other)
    {
        if (groundSpawner != null)
        {
            groundSpawner.SpawnTile();
            Destroy(gameObject, 2f);
        }
    }

    void SpawnObstacle()
    {
        if (firstTile)
        {
            firstTile = false;
            return;
        }

        if (tilesSinceLastObstacle < minEmptyTiles)
        {
            tilesSinceLastObstacle++;
            return;
        }

        if (Random.Range(0f, 1f) > 0.3f)
        {
            tilesSinceLastObstacle++;
            return;
        }

        tilesSinceLastObstacle = 0;

        int[] spawnIndices = { 3, 4, 5 };
        int obstacleSpawnIndex = spawnIndices[Random.Range(0, spawnIndices.Length)];

        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0), transform);
    }

    void SpawnCoins()
    {
        Vector3[] lanes =
        {
            new Vector3(-5.3f, 1f, 0), // Jalur kiri
            new Vector3(0.7f, 1f, 0),    // Jalur tengah
            new Vector3(6.7f, 1f, 0)   // Jalur kanan
        };

        int laneIndex = Random.Range(0, lanes.Length); // Pilih salah satu jalur

        for (int i = 0; i < 5; i++) // Spawn 5 koin di sepanjang jalur
        {
            Vector3 spawnPosition = transform.position + lanes[laneIndex] + new Vector3(0, 0, i * 2f);
            Quaternion rotation = Quaternion.Euler(-90, 0, 0); // Rotasi X -90 derajat

            Instantiate(coinPrefab, spawnPosition, rotation, transform);
        }
    }
}


