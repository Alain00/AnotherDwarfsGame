using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCont : MonoBehaviour
{
    public float NightDuration;
    public float NightMulti;
    public Text NighField;
    void Start()
    {
        NightMulti = PlayerPrefs.GetFloat("LvlCont");
        PlayerPrefs.SetFloat("LvlCont", NightMulti + .3f );
        NightDuration *= NightMulti + .3f; 
        
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
    void EndLVL(){
        Debug.Log("MissionDONE");
        //Cargar otra scene

    }
}
