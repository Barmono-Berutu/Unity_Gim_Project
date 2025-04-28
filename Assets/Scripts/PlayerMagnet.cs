using UnityEngine;
using System.Collections;

public class PlayerMagnet : MonoBehaviour
{
    public float magnetRadius = 8f;
    public bool isMagnetActive = false;
    public MagnetUIController uiController;


    public void ActivateMagnet(float duration)
    {
        if (uiController != null)
            uiController.ShowMagnet(duration);

        StartCoroutine(DeactivateAfter(duration));
    }


    IEnumerator DeactivateAfter(float duration)
    {
        isMagnetActive = true;
        yield return new WaitForSeconds(duration);
        isMagnetActive = false;
    }

    void Update()
    {
        if (!isMagnetActive) return;

        foreach (var hit in Physics.OverlapSphere(transform.position, magnetRadius))
            if (hit.CompareTag("Coin"))
                hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, 10f * Time.deltaTime);
    }
}
