using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMenu : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("weapon" , 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
