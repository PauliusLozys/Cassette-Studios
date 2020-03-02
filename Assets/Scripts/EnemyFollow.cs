using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D m_Rigidbody2D;
    public Transform wallCheck;
    public Transform groundCheck;
    public LayerMask whatIsWall;
    public float k_GroundedRadius = 0.5f;

    public float speed = 2f;
    public float stoppingDistance = 1.5f;
    public float m_JumpForce = 250;
    private bool m_FacingRight = true;
    private bool jump = false;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log(Vector2.Distance(transform.position, target.position).ToString());

        if (player.position.x > transform.position.x + stoppingDistance)
        {

            m_Rigidbody2D.position = new Vector2(m_Rigidbody2D.position.x + speed * Time.deltaTime, transform.position.y);
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (player.position.x < transform.position.x - stoppingDistance)
        {
            m_Rigidbody2D.position = new Vector2(m_Rigidbody2D.position.x - speed * Time.deltaTime, transform.position.y);
        }

        if (jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            jump = false;
        }
        if (Physics2D.OverlapCircle(wallCheck.position, k_GroundedRadius, whatIsWall) != null && Physics2D.OverlapCircle(groundCheck.position, k_GroundedRadius, whatIsWall) != null)
        {
            jump = true;
        }

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    switch (collision.tag)
    //    {
    //        case "Obsticle":
    //            Debug.Log("Collision!");
    //            jump = true;
    //            break;
    //        default:
    //            break;
    //    }
    //}
    private void LateUpdate()
    {
        SpriteFlip();
    }
    private void OnDrawGizmosSelected()
    {
        if (wallCheck is null)
            return;
        Gizmos.DrawWireSphere(wallCheck.position, k_GroundedRadius);
        Gizmos.DrawWireSphere(groundCheck.position, k_GroundedRadius);
    }
    private void SpriteFlip()
    {
        if (player.position.x > transform.position.x && !m_FacingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && m_FacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    { 
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
