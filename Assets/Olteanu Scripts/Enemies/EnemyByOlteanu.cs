using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyByOlteanu : MonoBehaviour
{

    public bool isGrounded;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
