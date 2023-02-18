using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource[] bgm;

    private void Start()
    {
        instance = this;
    }

    public void PlaySfx(int number)
    {
        sfx[number].Stop();
        sfx[number].Play();
    }

    public void PlayBgm(int number)
    {
        bgm[number].Stop();
        bgm[number].Play();
    }
}
