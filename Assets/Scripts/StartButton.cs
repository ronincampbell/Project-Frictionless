using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;
    public GameObject pressStart;

    public void startMenu()
    {
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        QuitButton.SetActive(true);
        pressStart.SetActive(false);
    }
}
