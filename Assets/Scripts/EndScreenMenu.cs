using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenMenu : MonoBehaviour
{
    // Load menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Reload menu
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
