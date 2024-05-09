using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceSoundBar : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    //public Image mute;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        //muted();
    }

    public void changeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        Debug.Log(value);
        //muted();
    }

    //public void muted()
    //{
    //    if (sliderValue == 0)
    //        mute.enabled = true;
    //    else
    //        mute.enabled = false;
    //}
}
