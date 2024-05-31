using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volumescript : MonoBehaviour
{
    [SerializeField] private AudioMixer MyAudioMixer;

    public void Setvolume(float Slidervalue)
    {
        MyAudioMixer.SetFloat("MasterVolume", Mathf.Log10(Slidervalue) * 20);
    }
}
