using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public SpriteRenderer theSprite;
    public GameObject deadSprite;

    private void Start()
    {
        
        theSprite = GetComponentInChildren<SpriteRenderer>();
        
    }
    
}
