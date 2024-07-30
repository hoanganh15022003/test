using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    [SerializeField] float moveSpeed;
    // float jumpHeight = 5f;
    bool isFacingRight = true;
    bool isGrounded = false;

    Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);


        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        if (rb.velocity.y < -0.01)
        {
            animator.SetBool("isJumping", true);

        }


        Debug.Log(rb.velocity.y);

        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    // private void FixedUpdate()
    // {
    //     rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    //     animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
    //     animator.SetFloat("yVelocity", rb.velocity.y);
    // }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = true;
            animator.SetBool("isJumping", !isGrounded);
        }
    }
}
