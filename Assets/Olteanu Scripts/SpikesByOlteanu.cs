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
                FindObjectOfType<LevelManagerByOlteanu>().AddDeadPlayer();
                FindObjectOfType<LevelManagerByOlteanu>().KillPlayer();
                FindObjectOfType<LevelManagerByOlteanu>().AddDeadPlayerColour();
                isOn = false;
                StartCoroutine(ResetSpikesCo());
            }
        }

    }

   IEnumerator ResetSpikesCo()
   {
        yield return new WaitForSeconds(.2f);
        isOn = true;
   }
}
