using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAtHighSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator Shake(){
        while(true){
         CameraShake.instance.ShouldShake = true;
         yield return null;
         }
    }
}
