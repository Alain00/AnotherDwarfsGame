using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutoriales : MonoBehaviour
{
    public GameObject[] Tutorials;
    int indice = 0;
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Tutorials[0].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Tutorials[indice].SetActive(false);
            indice++;
            audio.Play();
            if(indice == Tutorials.Length)
            SceneManager.LoadScene(0);
            Tutorials[indice].SetActive(true);
        }
    }
}
