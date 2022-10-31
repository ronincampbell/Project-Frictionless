using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using RetroAesthetics;


public class stopWatch : MonoBehaviour
{
    public bool stopwatchActive = false;
    float currentTime;
    public TextMeshProUGUI currentTimeText;
    public GameObject pCamera;
    public GameObject backdrop;
    public GameObject PressQ;
    public bool gameNotYetStart;


    private void Start()
    {

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
        if (stopwatchActive == true && !gameNotYetStart)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:ff");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

    private void CheckforQ()
    {
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
