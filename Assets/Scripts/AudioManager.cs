using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    public AudioClip[] _clips;


    void Awake()
    {
        _instance = this;
    }

    public void PlayMusic(int i)
    {
        this.GetComponent<AudioSource>().clip = _clips[i];
        this.GetComponent<AudioSource>().Play();
    }
}
