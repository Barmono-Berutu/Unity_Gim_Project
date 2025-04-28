using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public GameOverManager gameOverManager; // Drag GameOverManager di Inspector
    public AudioClip hitSound;              // Drag audio tabrakan di Inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Tambahkan AudioSource otomatis jika belum ada
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
}
