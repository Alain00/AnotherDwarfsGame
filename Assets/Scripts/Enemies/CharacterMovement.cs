using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public bool onlyRotateY = true;
    public Animator animator;
    CharacterController characterController;
    Vector3 lastDirection = Vector3.zero;
    Vector3 currentDirection = Vector3.zero;
    Vector3 lastPosition = Vector3.zero;
    public float speedChangeSpeed = 2;
    public float rotationChangeSpeed = 5;
    float gravityForce = 20.0f;
    float movingTime = 0;
    float currentSpeedPorcent = 0;
    float targetSpeedPorcent = 0;
    Quaternion currentRotation = Quaternion.identity;
    Quaternion targetRotation = Quaternion.identity;


    void Start(){
        characterController = GetComponent<CharacterController>();
    }

    public void MoveTo(Vector3 direction){
        if (direction == Vector3.zero){
            targetSpeedPorcent = 0;
        }else{
            targetSpeedPorcent = 1;
        }
        movingTime += Time.deltaTime;
        direction.y = -gravityForce;
        lastDirection = direction;
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
    }

    void FixedUpdate(){
        characterController.Move(lastDirection);
    }

    void LateUpdate(){
        lastPosition = transform.position;
    }

    void UpdateAnimator(){
        animator.SetFloat("speed", currentSpeedPorcent);
    }
}
