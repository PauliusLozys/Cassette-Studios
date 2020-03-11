using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    private Rigidbody2D m_Rigidbody2D;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    public void OnLanding()
    {
        animator.SetFloat("isJumping", 0.0f);
        animator.SetFloat("isFalling", 0.0f);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        animator.SetFloat("isJumping", m_Rigidbody2D.velocity.y);
        animator.SetFloat("isFalling", m_Rigidbody2D.velocity.y);
        animator.SetFloat("isWalking", Mathf.Abs(horizontalMove));
    }
}
