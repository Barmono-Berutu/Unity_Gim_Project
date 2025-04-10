using UnityEngine;

public class MagnetPowerUp : MonoBehaviour
{
    public float magnetDuration = 10f;

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        PlayerMagnet playerMagnet = other.GetComponent<PlayerMagnet>();
        if (playerMagnet != null)
        {
            playerMagnet.ActivateMagnet(magnetDuration);
            Destroy(gameObject);
        }
    }
}

}
