using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MakeDeadPlayerKinematic : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(KillMoveCo());
    }

    IEnumerator KillMoveCo()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.tag = "Ground";
    }


}
