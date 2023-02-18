using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float currentSpeed = 10.0f;
    public float speed = 10.0f;
    public float speedUp = 15.0f;
    public float jumpForce = 10.0f;
    public Rigidbody2D rigidBody2D;
    public GameObject checkPoint;
    public float horizontalInput;
    public float verticalInput;
    public bool canMove = true;
    public bool canJump = true;
    public bool canSpeed = true;
    public SpriteRenderer spriteRenderer;
    public float flyingForce = 5f;
    public LayerMask ground;
    public Vector3 boxSize;
    public float maxDistance;

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
            speed =  currentSpeed;
        }
        
        if(canMove)
        {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        }

         if (Input.GetKeyDown(KeyCode.W))
        {
            rigidBody2D.velocity = Vector2.up * jumpForce;
        }

        if(canJump)
        {
               if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
              Vector2 velocity = new Vector2(horizontalInput * speed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = velocity;
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

     void OnDrawGizmos() 
    {
     Gizmos.color =Color.red;
     Gizmos.DrawCube(transform.position-transform.up*maxDistance, boxSize);   
    }

     bool isGrounded()
    {
      if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, ground))
      {
        return true;
      }
      else
      {
        return false;
      }
    }
 }