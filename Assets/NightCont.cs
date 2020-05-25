using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCont : MonoBehaviour
{
    public float NightDuration;
    public float NightMulti;
    void Start()
    {
        NightMulti = PlayerPrefs.GetFloat("NightMulti");
        PlayerPrefs.SetFloat("NightMulti", NightMulti + .3f );
        NightDuration *= NightMulti + .3f; 
        
    }

    // Update is called once per frame
    void Update()
    {
        NightDuration -= Time.deltaTime;
        if(NightDuration == 0){
            EndLVL();
        }
    }
    void EndLVL(){
        Debug.Log("MissionDONE");
        //Cargar otra scene
        
    }
}
