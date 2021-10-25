using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager __instance__;
    public AudioSource soundEffectSource;
    public AudioSource musicSource;
    public void Start()
    {
        if (__instance__ != null)
            Debug.LogError("Multiple instance of sound manager");

        __instance__ = this;
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectSource.clip = clip;
        soundEffectSource.Play();
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void StopSoundEffect()
    {
        soundEffectSource.Stop();
    }

}
