﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    Animator anim;
    public AudioSource AttackSFX;
    public AudioSource MonsterGruarSFX;
    public AudioSource MonsterHited;
    float cooldown;
    EnemyAI EAI;
    void Start()
    {
       anim = GetComponent<Animator>(); 
       cooldown = Random.Range(5, 15);
       EAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        cooldown -= Time.deltaTime;
        if(cooldown <= 0){
            cooldown = Random.Range(5,15);
            MonsterGruarSFX.Play();
        }
        if(anim.GetBool("died"))
            MonsterGruarSFX.Stop();
        if(anim.GetInteger("attack") != 0){
            AttackSFX.Play();
        }

    }
}