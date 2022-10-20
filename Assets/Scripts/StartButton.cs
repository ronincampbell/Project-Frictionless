using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject PlayButtonSmall;
    public GameObject SettingsButton;
    public GameObject SettingsButtonSmall;
    public GameObject QuitButton;
    public GameObject QuitButtonSmall;
    public GameObject pressStart;
    public GameObject pressStartSmall;
    public GameObject triangle1;
    public GameObject triangle2;
    public GameObject triangle3;
    public GameObject triangle4;
    public GameObject triangle5;
    public GameObject triangle6;

    public void startMenu()
    {
        PlayButton.SetActive(true);
        PlayButtonSmall.SetActive(true);
        SettingsButton.SetActive(true);
        SettingsButtonSmall.SetActive(true);
        QuitButton.SetActive(true);
        QuitButtonSmall.SetActive(true);
        pressStart.SetActive(false);
        pressStartSmall.SetActive(false);
        triangle1.SetActive(false);
        triangle2.SetActive(false);
        triangle3.SetActive(false);
        triangle4.SetActive(false);
        triangle5.SetActive(false);
        triangle6.SetActive(false);
    }
}
