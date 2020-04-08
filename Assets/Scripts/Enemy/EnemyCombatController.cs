using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    public LayerMask whatIsDamageable;
    public Transform attackPoint;
    public Animator anim;

    public float attackRange = 3f;

    public float attackRate = 1; // attacks per second
    float nextAttackTime = 0f;
    //private bool isAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var tmp = Physics2D.OverlapCircle(attackPoint.position, attackRange, whatIsDamageable);

        if (Time.time >= nextAttackTime)
        {
            if (tmp != null)
            {
                //canAttack = true;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Player hit");
        anim.SetBool("canAttack", true);
    }

    //void CheckAttacks()
    //{
    //    anim.SetBool("canAttack", canAttack);
    //    canAttack = false;
    //    anim.SetBool("canAttack", canAttack);
    //}

    void FinnishAttack()
    {
        //canAttack = false;
        anim.SetBool("canAttack", false);

        Debug.Log("animation end");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint is null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
