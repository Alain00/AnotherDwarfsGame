using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyAI : Destructible
{
    [System.Serializable]
    public class AttackSettings{
        public int index = 1;
        public float cooldown = 1;
        public float animationOffset = 0.5f;
        public float damage = 10;
        public float distance = 2;
    }

    public Transform target;
    public float minDistance;
    public float minSpeed;
    public float maxSpeed;
    public AttackSettings[] attacks;
    public Transform attackOrigin;
    public LayerMask attackFilter;
    CharacterMovement characterMovement;
    float speed;
    float nexAttack = 0;
    bool hasDied = false;
    bool IsUnderFire = false;
    Animator animator;

    void Start(){
        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.SetSpeed(Random.Range(minSpeed, maxSpeed));
        animator = characterMovement.animator;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update(){
        if (hasDied) return;
        
        FollowTarget();
    }

    void FollowTarget(){
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        float distance = direction.magnitude;
        if (distance >= minDistance){
            characterMovement.MoveTo(target.position);
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
        AttackSettings settings = attacks[attackIndex];
        animator.SetInteger("attack", settings.index);
        nexAttack = Time.time + settings.cooldown;
        yield return new WaitForSeconds(settings.animationOffset);
        RaycastHit hit;
        if (Physics.Raycast(attackOrigin.position, attackOrigin.forward, out hit,settings.distance, attackFilter)){
            //Hit the player
            Collider col = hit.collider;
            if (col){
                PlayerStats stats = col.GetComponent<PlayerStats>();
                if (stats){
                    stats.ReciveDamage(settings.damage);
                }
            }
        }
        animator.SetInteger("attack", 0);
        Debug.Log("Success attack");
    }

    void OnDrawGizmosSelected(){
        if (attacks.Length == 0) return;
        Vector3 position = attackOrigin.position;
        for (int i = 0; i < attacks.Length; i++){
            float colorValue = (float)i/attacks.Length;
            Gizmos.color = new Color(colorValue, colorValue, colorValue);
            Gizmos.DrawRay(position, attackOrigin.forward * attacks[i].distance);
            position.y += 0.2f;
        }
    }

    public override void OnDie(){
        base.OnDie();
        characterMovement.enabled = false;
        animator.SetBool("died", true);
        Destroy(gameObject, 10);
    }

}
