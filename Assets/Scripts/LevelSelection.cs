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
    public GameObject Level3Button;
    public GameObject Level1MiniButton;
    public GameObject Level2MiniButton;
    public GameObject Level3MiniButton;
    public GameObject continueButton;
    public GameObject newGameButton;
    
    private void Start() 
    {
        // Get unlocked scenes from player data
        AvailScenes = PlayerPrefs.GetInt("UnlockedLevels");
        HideLevels();

        // If the player has no score, allow them to get a high score through setting it to 10000
        if (PlayerPrefs.GetFloat("HighscoreT") == 0)
            PlayerPrefs.SetFloat("HighscoreT", 10000);

        if (PlayerPrefs.GetFloat("Highscore1") == 0)
            PlayerPrefs.SetFloat("Highscore1", 10000);
        
        if (PlayerPrefs.GetFloat("Highscore2") == 0)
            PlayerPrefs.SetFloat("Highscore2", 10000);

        if (PlayerPrefs.GetFloat("Highscore3") == 0)
            PlayerPrefs.SetFloat("Highscore3", 10000);
    }

    // Display level tapes that the player has unlocked
    public void DisplayLevels()
    {
        if (AvailScenes == 2)
        {
            Level1Button.SetActive(true);
            Level1MiniButton.SetActive(true);
        }
        else if (AvailScenes == 3)
        {
            Level1Button.SetActive(true);
            Level2Button.SetActive(true);
            Level1MiniButton.SetActive(true);
            Level2MiniButton.SetActive(true);
        }
        else if (AvailScenes == 4)
        {
            Level1Button.SetActive(true);
            Level2Button.SetActive(true);
            Level3Button.SetActive(true);
            Level1MiniButton.SetActive(true);
            Level2MiniButton.SetActive(true);
            Level3MiniButton.SetActive(true);
        }
    }

    // Hide all levels
    public void HideLevels()
    {
        Level1Button.SetActive(false);
        Level2Button.SetActive(false);
        Level3Button.SetActive(false);
        Level1MiniButton.SetActive(false);
        Level2MiniButton.SetActive(false);
        Level3MiniButton.SetActive(false);
    }

    // Clear player data
    public void ClearProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        PlayerPrefs.SetFloat("HighscoreT", 10000);
        PlayerPrefs.SetFloat("Highscore1", 10000);
        PlayerPrefs.SetFloat("Highscore2", 10000);
        PlayerPrefs.SetFloat("Highscore3", 10000);
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
