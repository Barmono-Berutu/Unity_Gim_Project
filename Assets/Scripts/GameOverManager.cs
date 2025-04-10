using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
            Debug.Log("GameOverCanvas disembunyikan di awal.");
        }
        else
        {
            Debug.LogWarning("gameOverCanvas belum di-assign di Inspector!");
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver)
        {
            Debug.Log("Game over sudah aktif, abaikan.");
            return;
        }

        isGameOver = true;

        Time.timeScale = 0f;
        Debug.Log("Game di-pause (Time.timeScale = 0)");

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("gameOverCanvas NULL, tidak bisa tampil.");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart game...");
    }

    public void BackToDashboard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Kembali ke Main Menu...");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Keluar dari Play Mode (Editor)
#else
    Application.Quit(); // Keluar dari build
#endif
        Debug.Log("Keluar dari aplikasi");
    }

}
