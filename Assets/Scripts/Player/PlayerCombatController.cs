using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, rangedAttackCooldown;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    private float stunDamageAmount = 1f;
    [SerializeField]
    private float invulnerabilityTime = 1f;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;
    private float lastRangedAttackTime = Mathf.NegativeInfinity;

    private AttackDetails attackDetails;
    public float rangedAttackDamage = 20;

    private Animator anim;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private PlayerController PC;

    private PlayerStats playerStats;

    private bool isInvulnerable = false;
    private Renderer rend;
    private Color color;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
        rend = GetComponent<Renderer>();
        color = rend.material.color;
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

        if (Input.GetButtonDown("Fire1") && Time.time - lastRangedAttackTime > rangedAttackCooldown)
        {
            lastRangedAttackTime = Time.time;
            Shoot();
        }
    }

    void Shoot() //shooting logic
    {
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

        attackDetails.damageAmount = playerStats.GetPlayerDamage();
        attackDetails.position = this.transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;
        //attackDetails.position = transform.position;
        
        foreach (var  collider in detectedObjects)
        {

            collider.transform.parent.SendMessage("Damage", attackDetails);

        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }
    private void Damage(AttackDetails attackDetails)
    {
        //if (!PC.GetDashStatus())
       // {
            if (!isInvulnerable)
            {
                StartCoroutine("GetInvulnerable");
                int direction;

                playerStats.DecreaseHealth(Convert.ToInt32(attackDetails.damageAmount));

                if (attackDetails.position.x < transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }

                PC.Knockback(direction);
            }
            
      //  }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    public void SetCombat(bool state)
    {
        combatEnabled = state;
    }

    public float GetRangedAttackCooldown()
    {
        return rangedAttackCooldown;
    }
    IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(invulnerabilityTime);
        color.a = 1f;
        isInvulnerable = false;
        rend.material.color = color;
    }
}
