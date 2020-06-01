using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private AttackDetails attackDetails;

    private void Start()
    {
        attackDetails.damageAmount = 5;
        attackDetails.position = this.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("Damage", attackDetails);
            //StartCoroutine(player.Knockback(0.02f, 1300, player.transform.position));
        }
    }
}
