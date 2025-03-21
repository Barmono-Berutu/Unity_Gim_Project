using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;

    public void SpawnTile()
    {

        // Spawn tile di posisi yang sudah diperbaiki
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);

        // Update nextSpawnPoint berdasarkan posisi child
        nextSpawnPoint = temp.transform.GetChild(1).position;
    }

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }
}
