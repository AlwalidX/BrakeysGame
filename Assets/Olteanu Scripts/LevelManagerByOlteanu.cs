using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerByOlteanu : MonoBehaviour
{
    public bool isLevel1;
    public bool isLevel2;   

    public void Level1()
    {
        EnvironmentColourControl.instance.theSprite.color = new Color(1, 0.8f, 0.6f, 1);
    }

    public void Level2()
    {
        EnvironmentColourControl.instance.theSprite.color = new Color(1, 0.8f, 0.6f, 1);
    }
}
