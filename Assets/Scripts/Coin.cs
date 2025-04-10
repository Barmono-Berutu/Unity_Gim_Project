using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float collectionRadius = 2.5f;
    public float animationDuration = 0.15f;
    public float curveHeight = 2f;

    private GameObject backpack;
    private bool isAnimating = false;

    void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("BackPack");
    }

    void Update()
    {
        if (isAnimating || backpack == null) return;

        float distance = Vector3.Distance(transform.position, backpack.transform.position);
        PlayerMagnet playerMagnet = backpack.GetComponent<PlayerMagnet>();

        if ((distance <= collectionRadius) ||
            (playerMagnet != null && playerMagnet.isMagnetActive && distance <= playerMagnet.magnetRadius))
        {
            StartCoroutine(AnimateCoinToBackPack());
        }
    }


    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    private IEnumerator AnimateCoinToBackPack()
    {
        isAnimating = true;
        Vector3 startPoint = transform.position;
        Vector3 staticEndPoint = backpack.transform.position;

        Vector3 midPoint = (startPoint + staticEndPoint) / 2;
        midPoint.y += curveHeight;

        float elapsedTime = 0.0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            transform.position = CalculateBezierPoint(t, startPoint, midPoint, staticEndPoint);
            yield return null;
        }

        GameManager.inst.IncrementScore(); // Tambah skor setelah koin sampai ke backpack
        Destroy(gameObject);
    }
}
