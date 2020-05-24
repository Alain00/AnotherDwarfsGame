using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Item
{
    public int Ammo;
    public GameObject AntorchaDura;

    public override void ItemAction(Quaternion Dir){
        if(Ammo > 0)
        Instantiate(AntorchaDura , transform.position + transform.forward , Quaternion.identity);
    }
}
