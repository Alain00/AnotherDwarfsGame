using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    public float CurHealth;
    public GameObject HitSFX;
    void Start()
    {
        CurHealth = MaxHealth;
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.V))
            ReciveDamage(1);
    }
    public void ReciveDamage(float Damage){
        CurHealth -= Damage;
        GameObject Current = Instantiate(HitSFX,transform.position, Quaternion.identity);
        Destroy(Current , 2);
    }
}
