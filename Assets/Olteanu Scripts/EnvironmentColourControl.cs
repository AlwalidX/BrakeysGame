using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentColourControl : MonoBehaviour
{
    public static EnvironmentColourControl instance;

    public SpriteRenderer theSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theSprite = GetComponentInChildren<SpriteRenderer>();
       // FindObjectOfType<LevelManagerByOlteanu>()
    }
}
