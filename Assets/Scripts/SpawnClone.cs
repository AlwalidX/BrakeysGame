using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClone : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject clonedObject;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnObject();
        }
        */
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = transform.position;
        Instantiate(clonedObject, spawnPosition, Quaternion.identity);
    }
}
