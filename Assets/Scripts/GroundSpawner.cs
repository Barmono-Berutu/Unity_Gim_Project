using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public Transform player; // Tambahkan referensi ke player
    private Vector3 nextSpawnPoint;

    private void Start()
    {
        for (int i = 0; i < 100; i++) // Awal: spawn 15 tile
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).position;
    }

    private void Update()
    {
        if (player != null)
        {
            // Jika jarak player ke nextSpawnPoint lebih kecil dari 15, spawn tile baru
            if (Vector3.Distance(player.position, nextSpawnPoint) < 15)
            {
                SpawnTile();
            }
        }
    }
}
