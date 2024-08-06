using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();

    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(Settings.Volume);
    }

    private void Update()
    {
        PlayerPrefs.SetFloat(Settings.Volume, _slider.value);
    }
}
