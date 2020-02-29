using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public LayerMask playerMask;
    public Transform attackPoint;

    public float attackRange = 0.5f;

    public float attackRate = 2; // attacks per second
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var tmp = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask);
        
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
