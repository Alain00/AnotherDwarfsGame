using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NightCont : MonoBehaviour
{
    public float NightDuration;
    public float NightMulti;
    public Text NighField;
    ShipIsland Ship;
    EnemiesController enemiesController;
    
    void Awake(){
        Ship = GameObject.FindObjectOfType<ShipIsland>();
        NighField = GameObject.Find("NighDur").GetComponent<Text>();
    }
    void Start()
    {
        enemiesController = EnemiesController.main;
        NightMulti = PlayerPrefs.GetFloat("LvlCont");
        PlayerPrefs.SetFloat("LvlCont", NightMulti + .3f );
        NightDuration *= NightMulti + .3f; 
        enemiesController.OnWaveBegins += OnWaveBegins;
        enemiesController.OnWaveCompleted += OnWaveEnd;
        enemiesController.OnLastWaveCompleted += EndLVL;
    }

    // Update is called once per frame
    void Update()
    {   
        NightDuration -= Time.deltaTime;
        int minutes = Mathf.RoundToInt(NightDuration / 60);
        int seg = Mathf.RoundToInt(NightDuration - minutes * 60);
        NighField.text =  minutes.ToString() + ":" + seg.ToString();
        if(NightDuration == 0){
            EndLVL();
        }
    }

    void OnWaveEnd(){
        Ship.ComeBack();
    }

    void OnWaveBegins(){
        Ship.Leave();
    }

    void EndLVL(){
        Debug.Log("MissionDONE");
        //Cargar otra scene
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted");
        if (levelsCompleted < 0) levelsCompleted = 0;
        PlayerPrefs.SetInt("LevelsCompleted", levelsCompleted+1);

    }
}
