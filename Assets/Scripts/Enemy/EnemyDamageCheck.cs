using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCheck : MonoBehaviour
{
    AttackDetails attackDetails;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attackDetails.damageAmount = 30;
            attackDetails.position = this.transform.position;
            collision.transform.SendMessage("Damage", attackDetails);
        }
        
    }
}
