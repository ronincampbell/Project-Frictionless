using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    // Base Code by Brackeys on YouTube
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Slider SenseSlider;
    public Slider VolumeSlider;

    private void Start() 
    {
        LoadValues();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();


        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        PlayerPrefs.SetFloat("VolumeValue", volume);
        audioMixer.SetFloat("Volume", volume);
        LoadValues();
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSens(float sense)
    {
        PlayerPrefs.SetFloat("SenseValue", sense);
        LoadValues();
    }

    void LoadValues()
    {
        float sense = PlayerPrefs.GetFloat("SenseValue");
        float volume = PlayerPrefs.GetFloat("VolumeValue");
        SenseSlider.value = sense;
        VolumeSlider.value = volume;

    }
}
