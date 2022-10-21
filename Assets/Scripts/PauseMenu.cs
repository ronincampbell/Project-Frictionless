using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroAesthetics;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject Triangle1;
    public GameObject Triangle2;
    public GameObject Triangle3;
    public GameObject DeathScreen;
    public GameObject AliveUI;
    public GameObject pCamera;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !DeathScreen.activeInHierarchy)
        {
            PauseMenuUI.SetActive(true);
            Triangle1.SetActive(false);
            Triangle2.SetActive(false);
            Triangle3.SetActive(false);
            Time.timeScale = 0f;
            Cursor.visible = true;
            pCamera.GetComponent<RetroCameraEffect>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }       
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        AliveUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        pCamera.GetComponent<RetroCameraEffect>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
