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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distToPlayer < 10f)
        {
            if (playerTransform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
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
    }

   void OnCollisionEnter2D(Collision2D other)
    {

         if(other.gameObject.CompareTag("Player"))
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    } 

}


