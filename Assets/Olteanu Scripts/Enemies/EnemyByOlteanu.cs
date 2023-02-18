using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyByOlteanu : MonoBehaviour
{

    public bool isGrounded;
    public Animator anim;

    public Transform[] patroPoints;
    private int currentPoint;
    public float speed, waitAtPoint, jumpForce;
    private float waitCounter;
    public Rigidbody2D rb;
    public bool isOn;
    public GameObject theDeathEffect;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        waitAtPoint = Random.Range(.2f, 1);
        waitCounter = waitAtPoint;
        foreach (Transform pPoint in patroPoints)
        {
            pPoint.SetParent(null);
        }
    }


    private void Update()
    {
        if (Mathf.Abs(transform.position.x - patroPoints[currentPoint].position.x) > .15f)
        {
            if (transform.position.x < patroPoints[currentPoint].position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (transform.position.y < patroPoints[currentPoint].position.y - .1f && rb.velocity.y < 0.15f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                // anim.SetTrigger("jump");
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitAtPoint = Random.Range(.2f, 1);
                waitCounter = waitAtPoint;
                currentPoint++;
                if (currentPoint >= patroPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }


        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        if (collision.collider.CompareTag("Player"))
        {
           
            if (isOn)
            {
                AudioManager.instance.PlaySfx(2);
                Instantiate(theDeathEffect, collision.transform.position, collision.transform.rotation);
                collision.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                Destroy(collision.gameObject, 2.2f);
                collision.gameObject.GetComponent<PlayerControllByOlteanu>().canMove = false;
                //FindObjectOfType<LevelManagerByOlteanu>().AddDeadPlayerColour();
                isOn = false;
                StartCoroutine(ResetEnemyCo());
            }
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(gameObject, 5f);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    
    IEnumerator ResetEnemyCo()
    {
        
        yield return new WaitForSeconds(1f);
        FindObjectOfType<CameraController>().thePlayer = null;
        FindObjectOfType<CameraController>().virtualCamera.m_Follow = FindObjectOfType<CameraController>().spawnPoint;
        FindObjectOfType<LevelManagerByOlteanu>().spawnPlayer = true;
        FindObjectOfType<LevelManagerByOlteanu>().ResetPlayer();
        FindObjectOfType<LevelManagerByOlteanu>().KillPlayer();
        yield return new WaitForSeconds(.2f);
        isOn = true;
    }
   
}
