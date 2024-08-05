using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource[] _audioSource;

    void Awake()
    {
        _audioSource = GetComponents<AudioSource>();
    }

    void Update()
    {

        for (int i = 0; i < _audioSource.Length; i++)
        {
            _audioSource[i].volume = PlayerPrefs.GetFloat(Settings.Volume);
        }
        
    }
}
