using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyAi : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 3f;
    public float jumpForce = 300f;
    public float jumpDelay = 2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private float jumpTimer = 0f;
    public bool chase = false;
    public float patrolDistance = 2f;
    private Vector3 startPos;
    private Vector3 objectivePos;
    private bool walkingLeft;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        walkingLeft = true;
        objectivePos = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);

    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        

        if (chase && distToPlayer < 10f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            
            if (playerTransform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                walkingLeft= true;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                walkingLeft = false;

            }


            Vector3 direction = walkingLeft ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f, groundLayer);
            if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
            {
                if (jumpTimer <= 0f)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                    jumpTimer = jumpDelay;
                }
            }

            if (jumpTimer > 0f)
            {
                jumpTimer -= Time.deltaTime;
            }
        }
        else if(!chase)
        {
            //patroll
            if(!walkingLeft && transform.position.x >= startPos.x + patrolDistance)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                objectivePos = new Vector3(startPos.x - patrolDistance, transform.position.y, transform.position.z);
                walkingLeft = !walkingLeft;
            }
            else if(walkingLeft && transform.position.x <= startPos.x - patrolDistance)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                objectivePos = new Vector3(startPos.x + patrolDistance, transform.position.y, transform.position.z);
                walkingLeft = !walkingLeft;
            }
            transform.position = Vector2.MoveTowards(transform.position, objectivePos, moveSpeed * Time.deltaTime);

        }
    }

   void OnCollisionEnter2D(Collision2D other)
    {

         if(other.gameObject.CompareTag("Player"))
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    } 

}


