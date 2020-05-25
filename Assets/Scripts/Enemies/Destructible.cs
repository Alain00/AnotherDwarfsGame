using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destructible : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    bool died;
    public virtual void OnDamage(float damage){
        if (died) return;

        health -= damage;
        if (health <= 0){
            died = true;
            OnDie();
        }
    }

    public abstract void OnDie();
}
