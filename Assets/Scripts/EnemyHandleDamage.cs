using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleDamage : MonoBehaviour
{
    public EnemyStats stats;
    public void Start()
    {
        stats = GetComponentInChildren<EnemyStats>();
    }
    public void Damage(AttackDetails attackDetails)
    {
        stats.DecreaseHealth(attackDetails.damageAmount);
    }
}
