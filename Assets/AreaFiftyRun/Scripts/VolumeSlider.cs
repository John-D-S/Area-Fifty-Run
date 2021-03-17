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
    [SerializeField]
    private string volumeParameter;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.onValueChanged.AddListener(SliderValueChanged);
        slider.value = 0.3f;
    }

    private void SliderValueChanged(float _value)
    {
        group.SetFloat(volumeParameter, Remap(_value, 0f, 1f, -20, 20));
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
