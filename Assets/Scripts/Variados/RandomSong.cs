using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSong : MonoBehaviour
{
    public AudioClip[] songs;
    int indice;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EnemiesController.main.OnWaveBegins += SelectRandomSong;
        EnemiesController.main.OnWaveCompleted += StopSong;
        
    }

    void SelectRandomSong(){
        indice = Random.Range(0,songs.Length);
        audioSource.clip = songs[indice];
        audioSource.Play();
    }
    void StopSong(){
        audioSource.Stop();
    }
}
