using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomAudio();
    }

    public void PlayRandomAudio()
    {
        audioSource.clip = audioClips[UnityEngine.Random.Range(1, audioClips.Length)];
        audioSource.Play();
    }
}
