using UnityEngine;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;

    [Header("Prefabs")]
    public GameObject obstaclePrefab;
    public GameObject obstacleRoad;
    public GameObject coinPrefab;
    public GameObject magnetPrefab;

    [Header("Obstacle Settings")]
    [Range(1, 5)]
    public int minEmptyTiles = 3;

    private static int tilesSinceLastObstacle = 0;
    private static bool firstTile = true;

    private List<int> usedIndices = new List<int>();
    private HashSet<Vector3> usedPositions = new HashSet<Vector3>();

    private static int tilesSinceLastMagnet = 0;
    private const int minTilesBetweenMagnets = 10; // misalnya 10 tile

    private void Start()
    {
        groundSpawner = GameObject.FindFirstObjectByType<GroundSpawner>();
        if (groundSpawner == null)
        {
            Debug.LogError("GroundSpawner tidak ditemukan di scene!");
        }

        SpawnObstacles();
        SpawnCoins();
        SpawnMagnet();
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

        int maxObstacles = 2; // jumlah maksimum obstacle per tile
        int obstacleCount = Random.Range(1, maxObstacles + 1);

        for (int i = 0; i < obstacleCount; i++)
        {
            if (Random.value > 0.5f)
            {
                SpawnObstacle(obstaclePrefab, new int[] { 2, 3, 4 });
            }
            else
            {
                SpawnObstacle(obstacleRoad, new int[] { 5, 6, 7 });
            }
        }
    }

    private void SpawnObstacle(GameObject obstacle, int[] spawnIndices)
    {
        List<int> availableIndices = new List<int>();

        foreach (int index in spawnIndices)
        {
            if (!usedIndices.Contains(index))
            {
                availableIndices.Add(index);
            }
        }

        if (availableIndices.Count == 0) return;

        int chosenIndex = availableIndices[Random.Range(0, availableIndices.Count)];
        Transform spawnPoint = transform.GetChild(chosenIndex);

        Instantiate(obstacle, spawnPoint.position, Quaternion.identity, transform);

        usedIndices.Add(chosenIndex);
        usedPositions.Add(spawnPoint.position);
    }

    private void SpawnCoins()
    {
        Vector3[] lanes = {
            new Vector3(-5.3f, 1f, 0),
            new Vector3(0.7f, 1f, 0),
            new Vector3(6.7f, 1f, 0)
        };

        Vector3 lanePosition = lanes[Random.Range(0, lanes.Length)];
        int coinCount = Random.Range(3, 7);

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 spawnPosition = transform.position + lanePosition + new Vector3(0, 0, i * 2f);
            Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(-90, 0, 0), transform);
            usedPositions.Add(spawnPosition);
        }
    }

    private void SpawnMagnet()
    {
        tilesSinceLastMagnet++;

        if (tilesSinceLastMagnet < minTilesBetweenMagnets) return;

        if (Random.value >= 0.3f) return; // 30% chance

        // Pilihan posisi lokal berdasarkan data JSON (relative ke parent/track tile)
        Vector3[] localMagnetPositions = new Vector3[]
        {
        new Vector3(-5.93f, 0.63f, 8.77f), // kiri
        new Vector3(0.16f, 0.63f, 8.77f),  // tengah
        new Vector3(6.12f, 0.63f, 8.77f),  // kanan
        };

        // Ambil posisi acak dari ketiga posisi di atas
        Vector3 localMagnetPosition = localMagnetPositions[Random.Range(0, localMagnetPositions.Length)];
        Vector3 worldMagnetPosition = transform.TransformPoint(localMagnetPosition); // Ubah jadi world position

        foreach (var used in usedPositions)
        {
            if (Vector3.Distance(used, worldMagnetPosition) < 1f)
            {
                Debug.Log("Magnet position is occupied. Skipping spawn.");
                return;
            }
        }

        Instantiate(magnetPrefab, worldMagnetPosition, Quaternion.identity, transform);
        tilesSinceLastMagnet = 0;
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
