using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : Item
{
    public float LifeAmount;
    //public PlayerStats Player;
    public float Ammo;
    public PlayerStats Stats;
    void Start(){
        Stats = GameObject.FindObjectOfType<PlayerStats>();
    }
   public override void ItemAction(Quaternion Dir){
       if(Ammo <= 0){
          Debug.Log("NO AMMO");
       }
       else {
           Stats.CurHealth += LifeAmount;
           if(Stats.CurHealth > Stats.MaxHealth)
                Stats.CurHealth = Stats.MaxHealth;
           Ammo--; 
       }
   }
}
