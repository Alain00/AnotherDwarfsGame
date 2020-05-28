using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class ItemSpawOnDie : MonoBehaviour
{
    public GameObject[] items;
    public float noSpawProbabilities;
    Destructible destructible;

    void Start(){
        destructible = GetComponent<Destructible>();
        destructible.onDied += HandleOnDie;
    }

    void HandleOnDie(){
        GameObject item = items[Random.Range(0, items.Length)];
        float prod = Random.Range(0.0f, 1.0f);
        if (prod > noSpawProbabilities){
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
