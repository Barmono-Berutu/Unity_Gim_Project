using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    public int score;
    public static GameManager inst;

    public Text scoreText;

    public TMP_Text scoreGameOver;
    public void IncrementScore()
    {
        score++;

        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }
        if (scoreGameOver != null)
        {
            scoreGameOver.text = "SCORE: " + score;
        }
        else
        {
            Debug.LogError("scoreText belum diset di Inspector!");
        }
    }

    void Awake()
    {
        inst = this;
    }
}
