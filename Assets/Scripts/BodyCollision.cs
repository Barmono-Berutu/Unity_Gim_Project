using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    public GameOverManager gameOverManager; // Drag di Inspector
    public AudioClip hitSound;              // Drag file audio ke sini
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Tambahkan komponen AudioSource kalau belum ada
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
