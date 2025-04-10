using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public GameOverManager gameOverManager; // Tambahkan referensi GameOverManager


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {

            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver(); // Tampilkan UI Game Over
            }
        }
    }
}
