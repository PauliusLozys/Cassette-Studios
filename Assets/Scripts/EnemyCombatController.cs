using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    public LayerMask whatIsDamageable;
    public Transform attackPoint;

    public float attackRange = 3f;

    public float attackRate = 1; // attacks per second
    float nextAttackTime = 0f;

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
            }
        }
    }

    void Attack()
    {
        Debug.Log("Player hit");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint is null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
