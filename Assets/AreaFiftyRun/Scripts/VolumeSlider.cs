using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer group;

    [Header("Audio Group Controller")]
    [SerializeField]
    private string volumeParameter;
       
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up the slider
        slider = gameObject.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.onValueChanged.AddListener(SliderValueChanged);
        slider.value = 0.3f;
    }

    /// <summary>
    /// Sets the audio mixers volume by remaping the volume sliders value
    /// </summary>
    /// <param name="_value">Sliders value</param>
    private void SliderValueChanged(float _value)
    {
        group.SetFloat(volumeParameter, Remap(_value, 0f, 1f, -20, 20));
    }


    /// <summary>
    /// Remaps a float value from one range to another range.
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="from1">Inputs min value</param>
    /// <param name="to1">Inputs max value</param>
    /// <param name="from2">Outputs min value</param>
    /// <param name="to2">Outputs max value</param>
    /// <returns></returns>
    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
