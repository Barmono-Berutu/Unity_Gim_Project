using UnityEngine;
using TMPro;

public class MagnetUIController : MonoBehaviour
{
    public GameObject magnetPanel;
    public TextMeshProUGUI timerText;

    private float remainingTime;
    private bool isRunning;

    public void ShowMagnet(float duration)
    {
        remainingTime = duration;
        isRunning = true;
        magnetPanel.SetActive(true);
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;
        timerText.text = $"{remainingTime:F1}s";

        if (remainingTime <= 0f)
        {
            isRunning = false;
            magnetPanel.SetActive(false);
        }
    }
}
