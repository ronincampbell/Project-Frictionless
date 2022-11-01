using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RetroAesthetics;
using UnityEngine.SceneManagement;

public class EndLevelPickUp : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        highestlevel = PlayerPrefs.GetInt("UnlockedLevels");
    }

    private void Update() 
    {
        if (finalScore > 0)
        {
            HighScoreTime.text = HighScore.ToString();

            if (finalScore < HighScore)
            {
                Debug.Log("New HI SCORE!!!");
                PlayerPrefs.SetFloat("Highscore1", finalScore);
                HighScore = PlayerPrefs.GetFloat("Highscore1");
                HighScoreTime.text = HighScore.ToString();
                HighScoreText.SetActive(true);
            }
            else if (finalScore > HighScore)
            {
                Debug.Log("UNLUCKY");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pCamera.GetComponent<PlayerCam>().enabled = false;
        AliveUI.SetActive(false);
        Gun.SetActive(false);
        UnlockLevel();
        LevelEndUI();
        Debug.Log("Loading next scene...");

        finalScore = timer.GetComponent<stopWatch>().currentTime;
        PlayerObj.SetActive(false);

    }

    private void UnlockLevel()
    {
        if (highestlevel < levelNumber)
         {
            PlayerPrefs.SetInt("UnlockedLevels", levelNumber);
            Debug.Log(PlayerPrefs.GetInt("UnlockedLevels"));
         }
    }

    private void LevelEndUI()
    {
        endTime.text = runTimer.text;
        HighScore = PlayerPrefs.GetFloat("Highscore1");
        HighScoreUI.SetActive(true);
        pCamera.GetComponent<RetroCameraEffect>().enabled = true;
        footSteps.SetActive(false);
        

        Debug.Log("Your time was " + finalScore);
        Debug.Log("Your running HighScore is " + HighScore);
    }
}
