﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    
    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetails = new float[2];
    public float rangedAttackDamage = 20;

    private Animator anim;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private PlayerController PC;

    private PlayerStats playerStats;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!PauseMenu.GameIsPaused && !Pause.GameIsPaused)
        {
            CheckCombatInput();
            CheckAttacks();
        } 
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot() //shooting logic
    {
        Debug.Log("Ranged attack");

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            //Perform Attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }

        if(Time.time >= lastInputTime + inputTimer)
        {
            //Wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails[0] = playerStats.GetPlayerDamage();
        attackDetails[1] = transform.position.x;
        
        foreach (var  collider in detectedObjects)
        {

            collider.GetComponent<EnemyStats>().DecreaseHealth(playerStats.GetPlayerDamage());
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    public void SetCombat(bool state)
    {
        combatEnabled = state;
    }

}
