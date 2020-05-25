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
    CameraController cameraController;
   
    void Awake(){
        if(instance == null)
             instance = this;
    }

    void Start()
    {
        camara = Camera.main;
        cameraController = camara.GetComponent<CameraController>();
        startduration = duration;
        ShouldShake = false;
        startposition = camara.transform.localPosition;
    }

    void Update()
    {
        if(ShouldShake){
            if(duration > 0){
                duration -= Time.deltaTime * SlowDownAmount;
                camara.transform.localPosition = startposition + (Vector3)Random.insideUnitCircle * power;
            }
            else{
               // camara.transform.position =  startposition;
                duration =  startduration;
                ShouldShake = false;
         }
        }
       
    }
}
