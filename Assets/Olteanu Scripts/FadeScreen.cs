using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public Animator anim;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
