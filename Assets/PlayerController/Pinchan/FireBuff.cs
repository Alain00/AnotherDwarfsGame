using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : MonoBehaviour
{
    public int radius;
    public LayerMask mask;
    Collider[] colliders;

    public void OnTriggerStay(Collider col){
        if(col.gameObject.tag == "Enemy"){
            AgentPathFinding enemy = col.GetComponent<AgentPathFinding>();
            enemy.IsUnderFire = true;
        }
    }
    public void Update(){
       colliders = Physics.OverlapSphere(transform.position , radius , mask);
       if(colliders.Length > 0){
           for(int i = 0 ; i < colliders.Length ; i++){
               EnemyAI enemy = colliders[i].GetComponent<EnemyAI>();
               //enemy.IsUnderFire = true;
           }
           
       }
    }

    public void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.color = Color.red;
    }
}
