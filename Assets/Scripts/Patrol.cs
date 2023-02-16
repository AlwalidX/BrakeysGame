using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;
    EnemyAi enemyAi;
    private int randomSpot;
    private float waitTime;

    private void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        transform.LookAt(moveSpots[randomSpot]);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                transform.LookAt(moveSpots[randomSpot]);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime = -Time.deltaTime;
            }
        }
    
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distToPlayer < 4f)
        {enemyAi.ifClose = true;}
    
    }
}