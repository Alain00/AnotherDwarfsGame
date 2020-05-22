using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : MonoBehaviour
{
    public void OnTriggerStay(Collider col){
        if(col.gameObject.tag == "Enemy"){
            AgentPathFinding enemy = col.GetComponent<AgentPathFinding>();
            enemy.IsUnderFire = true;
        }
    }
}
