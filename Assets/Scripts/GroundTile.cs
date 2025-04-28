using UnityEngine;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{
    [Header("===[ References ]===")]
    private GroundSpawner groundSpawner;

    [Header("===[ Prefabs ]===")]
    public GameObject obstaclePrefab;
    public GameObject obstacleRoad;
    public GameObject coinPrefab;
    public GameObject magnetPrefab;
    

    [Header("===[ Obstacle Settings ]===")]
    [Range(1, 5)] public int minEmptyTiles = 3;

    private static int tilesSinceLastObstacle = 0;
    private static bool firstTile = true;

    [Header("===[ Magnet Settings ]===")]
    private static int tilesSinceLastMagnet = 0;
    private const int minTilesBetweenMagnets = 10;

    private List<int> usedIndices = new();
    private HashSet<Vector3> usedPositions = new();

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

        int obstacleCount = 2; // selalu 2 obstacle (satu dari masing-masing prefab)

        // Spawn satu dari obstaclePrefab
        SpawnObstacle(obstaclePrefab, new int[] { 2, 3, 4 });

        // Spawn satu dari obstacleRoad, tapi pastikan tidak terlalu dekat
        SpawnObstacle(obstacleRoad, new int[] { 5, 6, 7 });
    }

    private void SpawnObstacle(GameObject obstacle, int[] spawnIndices)
    {
        List<int> availableIndices = new();

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

        Vector3 spawnPosition = spawnPoint.position;

        if (IsPositionNearOther(spawnPosition, 1.5f)) return;

        Instantiate(obstacle, spawnPosition, Quaternion.identity, transform);
        usedIndices.Add(chosenIndex);
        usedPositions.Add(spawnPosition);
    }

    private void SpawnCoins()
    {
        Vector3[] lanes = {
            new Vector3(-5.3f, 2f, 4f),
            new Vector3(0.7f, 2f, 4f),
            new Vector3(6.7f, 2f, 4f)
        };

        Vector3 lane = lanes[Random.Range(0, lanes.Length)];
        int coinCount = Random.Range(3, 7);
        float zStartOffset = 6f; // makin depan dari obstacle

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                lane.x,
                lane.y,
                transform.position.z + zStartOffset + i * 2f
            );

            if (IsPositionNearOther(spawnPosition, 1f)) continue;

            Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(-90, 0, 0), transform);
            usedPositions.Add(spawnPosition);
        }
    }

    private void SpawnMagnet()
    {
        tilesSinceLastMagnet++;

        if (tilesSinceLastMagnet < minTilesBetweenMagnets) return;
        if (Random.value >= 0.3f) return;

        Vector3[] localMagnetPositions = {
            new Vector3(-5.93f, 0.63f, 8.77f),
            new Vector3(0.16f, 0.63f, 8.77f),
            new Vector3(6.12f, 0.63f, 8.77f)
        };

        Vector3 localMagnetPosition = localMagnetPositions[Random.Range(0, localMagnetPositions.Length)];
        Vector3 worldMagnetPosition = transform.TransformPoint(localMagnetPosition);

        if (IsPositionNearOther(worldMagnetPosition, 1.5f)) return;

        Instantiate(magnetPrefab, worldMagnetPosition, Quaternion.identity, transform);
        usedPositions.Add(worldMagnetPosition);
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

    private bool IsPositionNearOther(Vector3 position, float minDistance)
    {
        foreach (Vector3 used in usedPositions)
        {
            if (Vector3.Distance(used, position) < minDistance)
                return true;
        }
        return false;
    }
}
