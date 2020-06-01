using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject TutoButton;
    public GameObject StartGame;
    public GameObject Credits;
    public GameObject Exit;

    public GameObject RollCredits;
    public GameObject BackButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButton(){
        SceneManager.LoadScene(1);
    }
    public void OnTutoButton(){
        SceneManager.LoadScene(3);
    }
     public void OnCreditsButton(){
        StartGame.SetActive(false);
        Credits.SetActive(false);
        Exit.SetActive(false);
        RollCredits.SetActive(true);
        BackButton.SetActive(true);
    }
     public void OnExitButton(){
        Application.Quit();
    }
    public void OnBackButton(){
         StartGame.SetActive(true);
        Credits.SetActive(true);
        Exit.SetActive(true);
        RollCredits.SetActive(false);
        BackButton.SetActive(false);
    }
}
