using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourOfPlayer : MonoBehaviour
{
    public SpriteRenderer theSprite;

    private void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
        theSprite.color = FindObjectOfType<PlayerControllByOlteanu>().GetComponentInChildren<SpriteRenderer>().color;
        Destroy(theSprite, 4f);
    }
}
