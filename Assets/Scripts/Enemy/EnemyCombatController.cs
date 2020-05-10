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
    public float damage = 10f;
    private AttackDetails attackDetails;
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

                Attack();

            }
        }
    }

    void Attack()
    {
        Debug.Log("Player hit");
        anim.SetBool("canAttack", true);
        anim.SetBool("isAttacking", true);
    }
    public  void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsDamageable);

        attackDetails.damageAmount = damage;
        attackDetails.position = this.transform.position;

        foreach (var collider in detectedObjects)
        {

            collider.SendMessage("Damage", attackDetails);
        }
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
        anim.SetBool("isAttacking", false);

        Debug.Log("animation end");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint is null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
