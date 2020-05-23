using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   
    public static CameraShake instance;
    public float power = .4f;
    public Camera camara;
    public float duration = .3f;
    public float SlowDownAmount = 1f;
    public bool ShouldShake;
    public Vector3 startposition;
    float startduration;
    bool Once;
    void Awake(){
        if(instance == null)
             instance = this;
    }

    void Start()
    {
        camara = Camera.main;
        //startposition*= 0;
        startduration = duration;
        ShouldShake = false;
        Once = true;
       // startposition = camara.transform.position;
    }

    void Update()
    {
        if(ShouldShake){
            if(duration > 0){
                duration -= Time.deltaTime * SlowDownAmount;
                camara.transform.localPosition = startposition + Random.insideUnitSphere * power;
            }
            else{
               // camara.transform.position =  startposition;
                duration =  startduration;
                ShouldShake = false;
                Once = true;
         }
        }
       
    }
}
