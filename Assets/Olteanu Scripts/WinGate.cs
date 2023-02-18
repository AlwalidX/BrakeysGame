using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManagerByOlteanu>().YouWonTheGame();
            Debug.Log("Player Won");
        }
    }
}
