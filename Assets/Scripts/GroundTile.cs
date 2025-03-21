using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;

    private void Start()
    {
        groundSpawner = GameObject.FindAnyObjectByType<GroundSpawner>();

        if (groundSpawner == null)
        {
            Debug.LogError("GroundSpawner tidak ditemukan di scene!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit dipanggil oleh: " + other.gameObject.name);

        if (groundSpawner != null)
        {
            groundSpawner.SpawnTile();
            Destroy(gameObject, 2f);
        }
    }

}
