using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioSelector : MonoBehaviour
{
    public AudioSource[] audios;
    public int indice;
    void Start()
    {
        audios = GetComponents<AudioSource>();
        indice = Random.Range(0 , audios.Length);
        audios[indice].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
