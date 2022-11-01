using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private float AvailScenes;
    public GameObject Level1Button;
    public GameObject continueButton;
    public GameObject newGameButton;
    
    private void Start() 
    {
        AvailScenes = PlayerPrefs.GetInt("UnlockedLevels");
        HideLevels();

        if (PlayerPrefs.GetFloat("Highscore1") == 0)
            PlayerPrefs.SetFloat("Highscore1", 10000);
    }

    public void DisplayLevels()
    {
        if (AvailScenes > 0)
            Level1Button.SetActive(true);
    }

    public void HideLevels()
    {
        Level1Button.SetActive(false);
    }

    public void ClearProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        PlayerPrefs.SetFloat("Highscore1", 10000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void checkForContinue()
    {
        if (AvailScenes > 0)
        {
            continueButton.SetActive(true);
            newGameButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(false);
            newGameButton.SetActive(true);
        }

    }
}
