using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private PlayerStats playerStats;
    private AttackDetails attackDetails;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        anim = GetComponent<Animator>();
        rb.velocity = transform.right * playerStats.GetPlayerRangedSpeed();
        Debug.Log("Projectile spawned");
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag != "Player" && hitInfo.gameObject.tag != "LevelArea" && hitInfo.gameObject.tag != "PlayerProjectile")
        {
            Debug.Log("Detected: " + hitInfo.gameObject.tag);

            attackDetails.damageAmount = playerStats.GetPlayerRangedDamage();
            attackDetails.position = this.transform.position;
            attackDetails.stunDamageAmount = 0;

            if (hitInfo.gameObject.tag == "Enemy")
            {
                hitInfo.transform.parent.SendMessage("Damage", attackDetails);
            }
            rb.velocity = Vector2.zero;
            anim.SetBool("HitRegistered", true);
        }
    }

    void RemoveObject()
    {
        Destroy(gameObject);
    }
}
