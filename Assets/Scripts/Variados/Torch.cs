using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Item
{
    public GameObject AntorchaDura;

    public override void ItemAction(Quaternion Dir){
        if(Ammo > 0){
        Vector3 PosToInstan = SetPosInWorld.instance.SetPos(transform.position + transform.forward + transform.up * 10);
        Instantiate(AntorchaDura , PosToInstan  , Quaternion.identity);
        Ammo--;
        }
    }
}
