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
    private bool canAttack, attack1 = true, attack2 = false;

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
                nextAttackTime = Time.time + 1f / attackRate;
                canAttack = true;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Player hit");
        CheckAttacks();
    }

    void CheckAttacks()
    {
        anim.SetBool("attack1", attack1);
        anim.SetBool("attack2", attack2);
        anim.SetBool("canAttack", canAttack);
        attack1 = false;
        attack2 = true;
        canAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint is null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
