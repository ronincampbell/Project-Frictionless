using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    // Declare necessary variables
    private float AvailScenes;
    public GameObject Level1Button;
    public GameObject Level2Button;
    public GameObject continueButton;
    public GameObject newGameButton;
    
    private void Start() 
    {
        // Get unlocked scenes from player data
        AvailScenes = PlayerPrefs.GetInt("UnlockedLevels");
        HideLevels();

        // If the player has no score, allow them to get a high score through setting it to 10000
        if (PlayerPrefs.GetFloat("Highscore1") == 0)
            PlayerPrefs.SetFloat("Highscore1", 10000);
    }

    // Display level tapes that the player has unlocked
    public void DisplayLevels()
    {
        if (AvailScenes == 1)
            Level1Button.SetActive(true);
        else if (AvailScenes == 2)
        {
            Level1Button.SetActive(true);
            Level2Button.SetActive(true);
        }
    }

    // Hide all levels
    public void HideLevels()
    {
        Level1Button.SetActive(false);
        Level2Button.SetActive(false);
    }

    // Clear player data
    public void ClearProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        PlayerPrefs.SetFloat("Highscore1", 10000);
        PlayerPrefs.SetFloat("Highscore2", 10000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Check if the player has unlocked levels, if so display 'Continue' if not display 'New Game'
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
