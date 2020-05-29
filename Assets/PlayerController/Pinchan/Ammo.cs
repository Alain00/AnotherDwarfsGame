using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    PlayerController WeaponToBelong;
    public string WeaponName;
    public int AmmoCant;
    public int Index;
    void Start()
    {
        WeaponToBelong =GameObject.FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            WeaponToBelong.Weapons[Index].Ammo += AmmoCant;
            Destroy(gameObject);
        }
    }
}
