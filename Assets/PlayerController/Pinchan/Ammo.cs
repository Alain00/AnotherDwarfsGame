using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    Gun WeaponToBelong;
    public string WeaponName;
    public int AmmoCant;
    void Start()
    {
        WeaponToBelong = GameObject.Find(WeaponName).GetComponent<Gun>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            WeaponToBelong.Ammo += AmmoCant;
            Destroy(gameObject);
        }
    }
}
