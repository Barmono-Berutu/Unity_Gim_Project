using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> bgAudioClip;

    private void Start()
    {
        int index = Random.Range(0,bgAudioClip.Count);
        audioSource.clip = bgAudioClip[index];
        audioSource.volume = 0.2f;
        audioSource.Play();
    }
}
