using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    private AudioSource audioSrc;

    private float volume = .3f;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
    }

    void Update() {
        audioSrc.volume = volume;
    }

    public void setVolume(float vol) {
        volume = vol;
    }
}
