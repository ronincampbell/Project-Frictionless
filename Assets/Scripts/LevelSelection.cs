using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private float AvailScenes;
    public GameObject Level1Button;
    
    private void Start() 
    {
        AvailScenes = PlayerPrefs.GetInt("UnlockedLevels");
        HideLevels();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
