using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCont : MonoBehaviour
{
    public float NightDuration;
    public float NightMulti;
    public Text NighField;
    ShipIsland Ship;
    EnemiesController enemiesController;
    
    void Awake(){
        Ship = GameObject.FindObjectOfType<ShipIsland>();
    }
    void Start()
    {
        enemiesController = EnemiesController.main;
        NightMulti = PlayerPrefs.GetFloat("LvlCont");
        PlayerPrefs.SetFloat("LvlCont", NightMulti + .3f );
        NightDuration *= NightMulti + .3f; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            Ship.gameObject.SetActive(true);
        if(Input.GetKeyDown(KeyCode.L))
            Ship.leave = true;    
        
        NightDuration -= Time.deltaTime;
        int minutes = Mathf.RoundToInt(NightDuration / 60);
        int seg = Mathf.RoundToInt(NightDuration - minutes * 60);
        NighField.text =  minutes.ToString() + ":" + seg.ToString();
        if(NightDuration == 0){
            EndLVL();
        }
    }
    void EndLVL(){
        Debug.Log("MissionDONE");
        //Cargar otra scene

    }
}
