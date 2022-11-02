using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using RetroAesthetics;


public class stopWatch : MonoBehaviour
{
    // Declare necessary variables
    public bool stopwatchActive = false;
    public float currentTime;
    public TextMeshProUGUI currentTimeText;
    public GameObject pCamera;
    public GameObject backdrop;
    public GameObject PressQ;
    public bool gameNotYetStart;


    private void Start()
    {
        // Pause game once level loaded
        gameNotYetStart = true;
        Time.timeScale = 0f;
        pCamera.GetComponent<RetroCameraEffect>().enabled = true;
        backdrop.SetActive(true);
        PressQ.SetActive(true);
        currentTime = 0;
    }

    private void Update()
    {
        CheckforQ();
        // If game has been started, start the stopwatch and display its value in the currentTimeText variable
        if (stopwatchActive == true && !gameNotYetStart)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:ff");
        
    }

    // Start the stopwatch
    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    // Stop the stopwatch
    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

    private void CheckforQ()
    {
        // Wait until player press's 'Q' to start the game
        if(Input.GetKeyDown(KeyCode.Q) && gameNotYetStart)
        {
            gameNotYetStart = false;
            Time.timeScale = 1f;
            pCamera.GetComponent<RetroCameraEffect>().enabled = false;
            backdrop.SetActive(false);
            PressQ.SetActive(false);


        }
    }
}
