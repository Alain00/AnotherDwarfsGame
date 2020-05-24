using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : Item
{
    public float LifeAmount;
    //public PlayerStats Player;
    public float Ammo;
   public override void ItemAction(Quaternion Dir){
       if(Ammo <= 0){
           gameObject.SetActive(false);
       }
       else Debug.Log("Vida++");
   }
}
