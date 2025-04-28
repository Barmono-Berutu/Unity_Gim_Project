using UnityEngine;

public class Coin : MonoBehaviour
{
    public float collectionRadius = 2.5f;

    private Transform backpack;
    private AudioSource audioSource;
    public AudioClip audioCoinCollection;

    private bool isCollected = false; // Untuk mencegah multiple trigger

    void Start()
    {
        GameObject backpackObj = GameObject.FindGameObjectWithTag("BackPack");
        audioSource = GetComponent<AudioSource>();
        if (backpackObj != null)
            backpack = backpackObj.transform;
    }

    void Update()
    {
        if (backpack == null || isCollected) return;

        float distance = Vector3.Distance(transform.position, backpack.position);
        var magnet = backpack.GetComponent<PlayerMagnet>();

        float effectiveRadius = (magnet != null && magnet.isMagnetActive)
            ? Mathf.Max(collectionRadius, magnet.magnetRadius)
            : collectionRadius;

        if (distance <= effectiveRadius)
        {
            isCollected = true; // supaya tidak trigger berkali-kali
            GameManager.inst.IncrementScore();

            // Putar suara pengambilan koin
            if (audioSource != null && audioCoinCollection != null)
                audioSource.PlayOneShot(audioCoinCollection);

            // Hancurkan coin setelah suara selesai (delay sedikit atau pakai coroutine)
            Destroy(gameObject, audioCoinCollection.length); 
        }
    }
}
