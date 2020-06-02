using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAtHighSpeed : MonoBehaviour
{
    Animator animator;
    public bool MustGo;
    void Start()
    {
        StartCoroutine(Shake());
        animator = GetComponent<Animator>();
        animator.SetBool("EnterTrans",true);
    }

       void Update()
    {
       if(MustGo)
            animator.SetBool("EnterTrans",false);
    }

    IEnumerator Shake(){
        while(true){
         CameraShake.instance.ShouldShake = true;
         yield return null;
         }
    }
}
