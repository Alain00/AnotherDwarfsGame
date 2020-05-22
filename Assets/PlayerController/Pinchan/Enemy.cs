using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    void Start()
    {
        
    }

    void Update()
    {
        if(Health <= 0){
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
    }
}
