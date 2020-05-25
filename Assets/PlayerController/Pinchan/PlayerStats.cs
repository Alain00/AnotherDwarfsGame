using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    public float CurHealth;
    void Start()
    {
        CurHealth = MaxHealth;
    }
    public void ReciveDamage(float Damage){
        CurHealth -= Damage;
    }
}
