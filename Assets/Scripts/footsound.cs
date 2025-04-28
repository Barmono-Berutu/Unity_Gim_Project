using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class FootSound : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> footRunAudioClip;

    public void PlayFootSound(){
        int index = Random.Range(0,footRunAudioClip.Count);
        audioSource.volume = Random.Range(0.3f,0.7f);
        audioSource.PlayOneShot(footRunAudioClip[index]);
    }
}