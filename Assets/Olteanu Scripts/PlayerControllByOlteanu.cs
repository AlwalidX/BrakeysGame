using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllByOlteanu : MonoBehaviour
{
    public float currentSpeed = 10f;
    public float speed = 10f;
    public float speedUp = 20f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 7f;
    public bool isGrounded;
    public Rigidbody2D rigidBody2D;
    public GameObject checkPoint;
    public float horizontalInput;
    public float verticalInput;
    public bool canMove = true;
    public bool canJump = true;
    public bool canDoubleJump = true;
    private bool canDouble = true;
    public bool canSpeed = true;
    public SpriteRenderer spriteRenderer;
    public float flyingForce = 5f;
    public Animator anim;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (canSpeed)
        {
            speed = speedUp;
        }
        else
        {
            speed = currentSpeed;
        }

        if (canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
           
        }

        if (canJump)
        {
           
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    rigidBody2D.velocity = Vector2.up * jumpForce;
                    canDouble = true;
                }
                else if (canDoubleJump && canDouble)
                {
                    rigidBody2D.velocity = Vector2.up * jumpForce;
                    canDouble = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 velocity = new Vector2(horizontalInput * speed, rigidBody2D.velocity.y);
            rigidBody2D.velocity = velocity;
            if (rigidBody2D.velocity.x != 0)
            {
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
            }

            if (horizontalInput < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (horizontalInput > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canDouble = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
