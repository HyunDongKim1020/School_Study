using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip BGM;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    public void StopAUD() 
    {
        audioSource.Stop();
    }
    
}
