using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider soundSlider;

    public void ChangeVolume()
    {
        AudioListener.volume = soundSlider.value;
    }
}
