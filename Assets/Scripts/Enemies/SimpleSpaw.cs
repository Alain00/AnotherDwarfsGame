using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpaw : MonoBehaviour
{
    public EnemyAI[] prefab;
    public int count = 1;
    Transform player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.F1)){
            for (int i = 0; i < count; i++){
                EnemyAI toSpaw = prefab[Random.Range(0, prefab.Length)];
                EnemyAI newEnemy = Instantiate(toSpaw, transform.position, Quaternion.identity);
                newEnemy.target = player;
            }
        }
    }

    void OnGUI(){
        GUILayout.Label("Press F1 to spaw " + count + " enemies");
    }
}
