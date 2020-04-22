using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private PlayerStats playerStats;

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
        if (hitInfo.gameObject.tag != "Player"&& hitInfo.gameObject.tag != "LevelArea")
        {
            Debug.Log("Detected: " + hitInfo.name);
            rb.velocity = Vector2.zero;
            if (hitInfo.gameObject.tag == "Enemy")
            {
                hitInfo.GetComponent<EnemyStats>().DecreaseHealth(playerStats.GetPlayerRangedDamage());
            }

            anim.SetBool("HitRegistered", true);
        }
    }

    void RemoveObject()
    {
        Destroy(gameObject);
    }
}
