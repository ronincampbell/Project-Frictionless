using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroAesthetics;

public class PauseMenu : MonoBehaviour
{
    // Declare necessary variables
    public GameObject PauseMenuUI;
    public GameObject Triangle1;
    public GameObject Triangle2;
    public GameObject Triangle3;
    public GameObject DeathScreen;
    public GameObject AliveUI;
    public GameObject pCamera;
    public GameObject SettingsUI;
    public GameObject ResumeButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;
    public AudioSource audioSource;
    public GameObject PressQ;
    
    void Update()
    {
        // If player presses esape, pull up the pause menu
        if(Input.GetKeyDown(KeyCode.Escape) && !DeathScreen.activeInHierarchy && !PauseMenuUI.activeInHierarchy && !PressQ.activeInHierarchy)
        {
            PauseMenuUI.SetActive(true);
            ResumeButton.SetActive(true);
            SettingsButton.SetActive(true);
            QuitButton.SetActive(true);
            Triangle1.SetActive(false);
            Triangle2.SetActive(false);
            Triangle3.SetActive(false);
            Time.timeScale = 0f;
            Cursor.visible = true;
            pCamera.GetComponent<RetroCameraEffect>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            audioSource.volume = 0.1f;
        }
        // If player presses escape and the pause menu is already active, resume the game
        else if(Input.GetKeyDown(KeyCode.Escape) && !DeathScreen.activeInHierarchy && !PressQ.activeInHierarchy)
        {
            Resume();
        }       
    }

    // Hide the pause menu, set time back to normal and show the alive UI
    public void Resume()
    {
        SettingsUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        AliveUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        pCamera.GetComponent<RetroCameraEffect>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        audioSource.volume = 1f;
    }
}
