using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    public List <GameObject> Drop;
    
    void Start()
    {
         Random.InitState(System.DateTime.Now.Millisecond);
    }

    void Update()
    {
        if(Health <= 0){
            Die();
        }
    }
    void Die(){
        int number = Random.Range(1,8);
        Debug.Log(number);
        if(number > 6){
            number = Random.Range(0,Drop.Count);
            Instantiate(Drop[number],transform.position , Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
