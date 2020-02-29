using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D m_Rigidbody2D;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public float speed = 2f;
    public float stoppingDistance = 0.2f;
    public float m_JumpForce = 350f;
    private bool m_FacingRight = true;
    private bool jump = true;
    const float k_GroundedRadius = .02f;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if(target.position.y > transform.position.y && jump && Vector2.Distance(transform.position, target.position) > 0.5)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            jump = !jump;
        }

        SpriteFlip();

        if(Physics2D.OverlapCircle(groundCheck.position, k_GroundedRadius, whatIsGround) != null)
        {
            jump = true;
        }

    }

    private void SpriteFlip()
    {
        if (target.position.x > transform.position.x && !m_FacingRight)
        {
            Flip();
        }
        else if (target.position.x < transform.position.x && m_FacingRight)
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
