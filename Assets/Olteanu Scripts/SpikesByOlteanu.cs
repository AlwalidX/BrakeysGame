using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesByOlteanu : MonoBehaviour
{
    public bool isOn = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
                StartCoroutine(ResetSpikesCo());
            }
        }

    }

   IEnumerator ResetSpikesCo()
   {
        yield return new WaitForSeconds(1f);
        isOn = true;
   }
}
