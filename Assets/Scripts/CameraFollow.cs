using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Objek yang akan diikuti (Player)
    public Vector3 offset ;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
