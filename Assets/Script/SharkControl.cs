﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkControl : MonoBehaviour, Respawnable
{
    Transform player;

    public float searchDistance = 100;
    public float speed = 0.05f;
    int attackCounter;
    int attackCD = 150;
    int attackCharge = 180;
    int attackDuration = 220;
    float attackSpeed = 10f;
    float targetRotationY;
    float angle;
    public int shockCounter;
    public int shockDuration = 100;
    Animator animator;
    AudioSource attackSound;
    public AudioSource stunSound;
    public Vector3 respawnPos = new Vector3(0,0,0);
    bool attacked;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
        respawn();
    }

    private void FixedUpdate() {
        Vector3 diff = player.position - transform.position;
        if (Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2) < searchDistance && shockCounter == 0){
            attackCounter ++;
            if (attackCounter >= attackCD && attackCounter < attackCharge){
                animator.SetBool("inCharge", true);
                if (transform.eulerAngles.y > 90){
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                }else{
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                }

            }else if (attackCounter >= attackCharge && attackCounter < attackDuration)
            {
                animator.SetBool("inAttack", true);
                if (!attacked)
                {
                    attacked = true;
                    attackSound.Play();
                }
                transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * speed * attackSpeed, transform.position.y + Mathf.Sin(angle) * speed * attackSpeed, transform.position.z);
            }else if (attackCounter >= attackDuration)
            {
                attackCounter = 0;
                attacked = false;
            }
            if (attackCounter < attackCD){
                animator.SetBool("inCharge", false);
                animator.SetBool("inAttack", false);
                angle = Mathf.Atan2(diff.y, diff.x);
                if (Mathf.Abs(angle) > Mathf.PI / 2)
                {
                    targetRotationY = 180;
                    float rotationY = Mathf.Lerp(transform.eulerAngles.y, targetRotationY, 0.1f);
                    transform.eulerAngles = new Vector3(0, rotationY, 180 - angle * Mathf.Rad2Deg);
                }
                else
                {
                    targetRotationY = 0;
                    float rotationY = Mathf.Lerp(transform.eulerAngles.y, targetRotationY, 0.1f);
                    transform.eulerAngles = new Vector3(0, rotationY, angle * Mathf.Rad2Deg);
                }
                transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * speed, transform.position.y + Mathf.Sin(angle) * speed, transform.position.z);       
            }
            
        }else
        {
            attackCounter = 0;
            attacked = false;
            if (shockCounter > 0){
                shockCounter --;
            }
            if (shockCounter == 0){
                animator.SetBool("inShock", false);
            }
            animator.SetBool("inCharge", false);
            animator.SetBool("inAttack", false);
        }
    }

    public void StartShock(){
        shockCounter = shockDuration;
        stunSound.Play();
        animator.SetBool("inShock", true);
    }

    public void respawn(){
        attackCounter = 0;
        shockCounter = 0;
        attacked = false;
        transform.position = respawnPos;
        transform.eulerAngles = new Vector3(0,0,0);
    }
}
