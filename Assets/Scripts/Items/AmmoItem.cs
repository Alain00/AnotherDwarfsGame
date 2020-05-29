using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    GameObject player;
    PlayerController controller;


    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        List<GameObject> ammos = new List<GameObject>();
        foreach (Gun gun in controller.Weapons){
            if (!gun.ammoItem) continue;
            if (gun.Comprada){
                ammos.Add(gun.ammoItem);
            }
        }
        Instantiate(ammos[Random.Range(0, ammos.Count)], transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
