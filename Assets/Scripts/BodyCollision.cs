using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    public GameOverManager gameOverManager; // drag di Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {

            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
}
