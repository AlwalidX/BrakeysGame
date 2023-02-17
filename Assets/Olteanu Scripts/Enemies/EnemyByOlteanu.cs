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


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitAtPoint;
        foreach (Transform pPoint in patroPoints)
        {
            pPoint.SetParent(null);
        }
    }


    private void Update()
    {
        if (Mathf.Abs(transform.position.x - patroPoints[currentPoint].position.x) > .1f)
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

            if (transform.position.y < patroPoints[currentPoint].position.y - .1f && rb.velocity.y < 0.1f)
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
        /*
        if (collision.collider.CompareTag("Player"))
        {
            if (isOn)
            {
                FindObjectOfType<CameraController>().thePlayer = null;
                FindObjectOfType<CameraController>().virtualCamera.m_Follow = FindObjectOfType<CameraController>().spawnPoint;
                FindObjectOfType<LevelManagerByOlteanu>().spawnPlayer = true;
                FindObjectOfType<LevelManagerByOlteanu>().ResetPlayer();
                FindObjectOfType<LevelManagerByOlteanu>().KillPlayer();
                FindObjectOfType<LevelManagerByOlteanu>().AddDeadPlayer();
                isOn = false;
                StartCoroutine(ResetEnemyCo());
            }
        }
        */
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    /*
    IEnumerator ResetEnemyCo()
    {
        yield return new WaitForSeconds(.5f);
        isOn = true;
    }
    */
}
