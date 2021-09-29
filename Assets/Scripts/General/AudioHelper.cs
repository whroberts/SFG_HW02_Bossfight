using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioSource PlayClip2D(AudioClip clip, string name, float volume, float time, float startTime = 0f)
    {
        //create
        GameObject audioObject = new GameObject("Audio2D: " + name);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        //configure
        audioSource.clip = clip;
        audioSource.volume = volume;

        //activate
        audioSource.Play();
        audioSource.time = startTime;
        Object.Destroy(audioObject, time);

        //return in case other things need it
        return audioSource;
    }
}
