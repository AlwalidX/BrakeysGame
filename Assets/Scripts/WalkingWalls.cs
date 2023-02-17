using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingWalls : MonoBehaviour
{
    public float currentSpeed = 10.0f;
    private float speed = 10.0f;
    public float speedWall = 5.0f;
    public float speedUp = 15.0f;
    public float jumpForce = 10.0f;
    public Rigidbody2D rigidBody2D;
    public GameObject checkPoint;
    private float horizontalInput;
    private float verticalInput;
    private bool canMove = true;
    private bool canJump = true;
    private bool canSpeed = true;
    private bool isJumpingWall = false;
    public SpriteRenderer spriteRenderer;
    public float flyingForce = 5f;
    public bool isGrounded = false;

    public LayerMask wallLayer;
    private bool isWallLeft = false;
    private bool isWallRight = false;
    public bool canMoveWall = false;
    public float maxSpeed= 50f;



    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidBody2D.velocity = Vector2.up * jumpForce;
        }

        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        if (isWallLeft || isWallRight)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                rigidBody2D.AddForce(new Vector2(jumpForce * (isWallRight ? -1f : 1f), jumpForce ), ForceMode2D.Impulse);
                isJumpingWall = true;
                
            }
        }
;
        if (isJumpingWall && (!(isWallLeft || isWallRight)))
        {

            isJumpingWall = false;
            canMove = true;
        }
            

    }

    private void FixedUpdate()
    {
        CheckWalls();


        if(!isJumpingWall && (isWallLeft || isWallRight))
        {
            rigidBody2D.gravityScale = 0;        
            Vector2 velocity = new Vector2(rigidBody2D.velocity.x, canMoveWall ? verticalInput * speedWall : 0);
            rigidBody2D.velocity = velocity;
            canMove = false;
        }
        else
        {
            rigidBody2D.gravityScale = 1f;
        }

        if (canMove)
        {
            rigidBody2D.AddForce(new Vector2(horizontalInput * speed, 0));
            if (horizontalInput > 0 && rigidBody2D.velocity.x > maxSpeed)
                rigidBody2D.velocity = new Vector2(maxSpeed, rigidBody2D.velocity.y);
            else if (horizontalInput < 0 && rigidBody2D.velocity.x < -maxSpeed)
                rigidBody2D.velocity = new Vector2(-maxSpeed, rigidBody2D.velocity.y);
            else if(horizontalInput == 0)
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x * 0.9f, rigidBody2D.velocity.y);

        }

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    private void CheckWalls()
    {
        Vector3 directionLeft = new Vector3(-1f, 0, 0);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, directionLeft, 1.15f, wallLayer);

        Vector3 directionRight = new Vector3(1f, 0, 0);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, directionRight, 1.15f, wallLayer);

        if (hitLeft.collider != null)
        {
            isWallLeft = true;
        }
        else
        {
            isWallLeft = false;
        }

        if (hitRight.collider != null)
        {
            isWallRight = true;
        }
        else
        {
            isWallRight = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
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