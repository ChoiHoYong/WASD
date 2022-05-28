using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Effect"))
        {
            //musicvolume
            PlayerPrefs.SetFloat("Effect", 1);
            Load();
        }

    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Effect");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Effect", volumeSlider.value);
    }

    
}
