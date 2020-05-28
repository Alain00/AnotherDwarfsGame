using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    public bool onlyRotateY = true;
    public Animator animator;
    NavMeshAgent agent;
    public float speedChangeSpeed = 2;
    public float rotationChangeSpeed = 5;
    float gravityForce = 20.0f;
    float movingTime = 0;
    float currentSpeedPorcent = 0;
    float targetSpeedPorcent = 0;
    Quaternion currentRotation = Quaternion.identity;
    Quaternion targetRotation = Quaternion.identity;
    Collider mCollider;
    Vector3 destination;

    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        mCollider = GetComponent<Collider>();
    }

    public void SetSpeed(float speed){
        agent.speed = speed;
    }

    public void MoveTo(Vector3 newDestination){
        if (newDestination != Vector3.zero){
            targetSpeedPorcent = 1;
        }
        destination = newDestination;
    }

    public void RotateTo(Vector3 direction){
        Quaternion rotation = Quaternion.LookRotation(direction, transform.up);
        if (onlyRotateY){
            rotation.x = 0;
            rotation.z = 0;
        }
        targetRotation = rotation;
    }

    public void RotateTo(Quaternion rotation){
        targetRotation = rotation;
    }


    void Update(){
        // currentDirection = characterController.velocity;
        // currentDirection.y = 0;
        // Debug.Log(currentDirection);
        currentSpeedPorcent = Mathf.Lerp(currentSpeedPorcent, targetSpeedPorcent, speedChangeSpeed * Time.deltaTime);
        
        currentRotation = Quaternion.Lerp(currentRotation, targetRotation, rotationChangeSpeed * Time.deltaTime);
        transform.rotation = currentRotation;
        
        UpdateAnimator();
        agent.SetDestination(destination);
    }

    void OnDisable(){
        agent.enabled = false;
        if (mCollider){
            mCollider.enabled = false;
        }
    }

    void UpdateAnimator(){
        animator.SetFloat("speed", currentSpeedPorcent);
    }
}
