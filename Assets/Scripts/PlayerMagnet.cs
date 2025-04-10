using UnityEngine;
using System.Collections;

public class PlayerMagnet : MonoBehaviour
{
    public float magnetRadius = 8f;
    public bool isMagnetActive = false;

    public void ActivateMagnet(float duration)
    {
        StartCoroutine(MagnetCoroutine(duration));
    }

    private IEnumerator MagnetCoroutine(float duration)
    {
        isMagnetActive = true;
        yield return new WaitForSeconds(duration);
        isMagnetActive = false;
    }

    void Update()
    {
        if (isMagnetActive)
        {
            AttractCoins();
        }
    }

    void AttractCoins()
    {
        Collider[] coins = Physics.OverlapSphere(transform.position, magnetRadius);
        foreach (var coin in coins)
        {
            if (coin.CompareTag("Coin"))
            {
                coin.transform.position = Vector3.MoveTowards(
                    coin.transform.position,
                    transform.position,
                    10f * Time.deltaTime
                );
            }
        }
    }

}
