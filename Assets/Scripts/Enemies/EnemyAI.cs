using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyAI : MonoBehaviour
{
    [System.Serializable]
    public class AttackSettings{
        public float cooldown = 1;
        public float animationOffset = 0.5f;
        public float damage = 10;
    }

    public Transform target;
    public float minDistance;
    public float minSpeed;
    public float maxSpeed;
    public AttackSettings[] attacks;
    CharacterMovement characterMovement;
    float speed;
    float nexAttack = 0;
    Animator animator;

    void Start(){
        characterMovement = GetComponent<CharacterMovement>();
        animator = characterMovement.animator;
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
            OrderAttack();
        }
        // Debug.Log(distance);
        characterMovement.RotateTo(direction.normalized);
    }

    void OrderAttack(){
        if (attacks.Length == 0) return;
        if (Time.time < nexAttack) return;
        StartCoroutine(ExecuteAttack());
    }

    IEnumerator ExecuteAttack(){
        int attackIndex = Random.Range(0, attacks.Length);
        animator.SetInteger("attack", attackIndex+1);
        AttackSettings settings = attacks[attackIndex];
        nexAttack = Time.time + settings.cooldown;
        yield return new WaitForSeconds(settings.animationOffset);

        animator.SetInteger("attack", 0);
        Debug.Log("Success attack");
    }

}
