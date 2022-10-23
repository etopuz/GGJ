using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioData;
    [SerializeField] AudioClip[] audios;
    private void Start() {
        
        audioData = GetComponent<AudioSource>();
        audioData.clip = audios[0];
        audioData.loop = true;
        audioData.Play();
    }

    public void GameStart()
    {
        audioData.Stop();
        
        audioData.clip = audios[1];
        audioData.loop = true;
        audioData.Play();
    }

    public void StopSound(){
        audioData.Stop();
    }
}
