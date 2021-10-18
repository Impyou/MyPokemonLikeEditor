using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager __instance__;
    public AudioSource audioSource;
    public void Start()
    {
        if (__instance__ != null)
            Debug.LogError("Multiple instance of sound manager");

        __instance__ = this;
    }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }


}
