using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int score;
    public static GameManager inst;

    public Text scoreText;
    public void IncrementScore()
    {
        score++;

        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
