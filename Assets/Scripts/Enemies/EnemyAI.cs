using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float minDistance;
    public float minSpeed;
    public float maxSpeed;
    CharacterMovement characterMovement;
    float speed;

    void Start(){
        characterMovement = GetComponent<CharacterMovement>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update(){
        FollowTarget();
    }

    void FollowTarget(){
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        float distance = direction.magnitude;
        if (distance >= minDistance){
            characterMovement.MoveTo(direction.normalized * speed * Time.deltaTime);
        }else{
            characterMovement.MoveTo(Vector3.zero);
        }
        // Debug.Log(distance);
        characterMovement.RotateTo(direction.normalized);
    }
}
