using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RetroAesthetics;
using UnityEngine.SceneManagement;

public class EndLevelPickUp : MonoBehaviour
{
    // Declare necessary variables
    public int levelNumber;
    private int highestlevel;
    private float finalScore;
    private float HighScore;
    public TextMeshProUGUI endTime;
    public TextMeshProUGUI runTimer;
    public TextMeshProUGUI HighScoreTime;
    public GameObject HighScoreUI;
    public GameObject AliveUI;
    public GameObject timer;
    public GameObject HighScoreText;
    public GameObject PlayerObj;
    public GameObject Gun;
    public GameObject pCamera;
    public GameObject footSteps;

    // Check players highest unlocked level
    void Start()
    {
        highestlevel = PlayerPrefs.GetInt("UnlockedLevels");
    }

    // Check if current runs score beats previous high score
    private void Update() 
    {
        if (finalScore > 0)
        {
            HighScoreTime.text = HighScore.ToString();

            if (finalScore < HighScore)
            {
                PlayerPrefs.SetFloat("Highscore1", finalScore);
                HighScore = PlayerPrefs.GetFloat("Highscore1");
                HighScoreTime.text = HighScore.ToString();
                HighScoreText.SetActive(true);
            }
        }
    }

    // Activate primary UI elements and save players score
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pCamera.GetComponent<PlayerCam>().enabled = false;
        AliveUI.SetActive(false);
        Gun.SetActive(false);
        UnlockLevel();
        LevelEndUI();
        finalScore = timer.GetComponent<stopWatch>().currentTime;
        PlayerObj.SetActive(false);

    }


    //Unlock level if the player doesnt have the current one unlocked yet
    private void UnlockLevel()
    {
        if (highestlevel < levelNumber)
         {
            PlayerPrefs.SetInt("UnlockedLevels", levelNumber);
            Debug.Log(PlayerPrefs.GetInt("UnlockedLevels"));
         }
    }

    // Display secondary UI elements and mute footsteps
    private void LevelEndUI()
    {
        endTime.text = runTimer.text;
        HighScore = PlayerPrefs.GetFloat("Highscore1");
        HighScoreUI.SetActive(true);
        pCamera.GetComponent<RetroCameraEffect>().enabled = true;
        footSteps.SetActive(false);
    }
}
