using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeringeSpawner : MonoBehaviour
{
    public Transform seringeSpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManagerByOlteanu>().nearSpawnPoint = true;
        }

        
        if(other.CompareTag("DeadPlayer"))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManagerByOlteanu>().nearSpawnPoint = false;
        }
    }
}
