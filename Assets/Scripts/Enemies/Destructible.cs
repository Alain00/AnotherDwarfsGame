using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destructible : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    bool died;

    public delegate void OnDied();
    public event OnDied onDied;
    public virtual void OnDamage(float damage){
        if (died) return;

        health -= damage;
        if (health <= 0){
            died = true;
            OnDie();
        }
    }

    public virtual void OnDie(){
        if (onDied != null)
            onDied();
    }
}
