using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class stopWatch : MonoBehaviour
{
    public bool stopwatchActive = false;
    float currentTime;
    public TextMeshProUGUI currentTimeText;


    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        if (stopwatchActive == true)
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
}
