using UnityEngine;
using UnityEngine.UI;

public class KeyboardUIFeedback : MonoBehaviour
{
    [Header("Tombol UI")]
    public Image btnW;
    public Image btnA;
    public Image btnS;
    public Image btnD;

    [Header("Warna")]
    public Color normalColor = Color.white;
    public Color pressedColor = Color.gray;

    private void Update()
    {
        SetButtonState(btnW, KeyCode.W);
        SetButtonState(btnA, KeyCode.A);
        SetButtonState(btnS, KeyCode.S);
        SetButtonState(btnD, KeyCode.D);
    }

    private void SetButtonState(Image btn, KeyCode key)
    {
        if (Input.GetKey(key))
        {
            btn.color = pressedColor;
            btn.transform.localScale = Vector3.one * 1.1f; // Sedikit besar saat ditekan
        }
        else
        {
            btn.color = normalColor;
            btn.transform.localScale = Vector3.one;
        }
    }
}
