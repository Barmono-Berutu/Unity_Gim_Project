using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        GameManager.inst.IncrementScore(); // Panggil IncrementScore() 

        Destroy(gameObject);
    }


    void Update()
    {
        // Memutar koin agar tampak berputar saat melayang
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
