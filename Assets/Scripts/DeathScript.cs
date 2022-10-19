using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using RetroAesthetics;

public class DeathScript : MonoBehaviour
{
    public GameObject pCamera;
    public GameObject DeathScreen;
    public GameObject AliveUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ResetGame();
        }
    }
    public void PlayerDie()
    {
        DeathScreen.SetActive(true);
        AliveUI.SetActive(false);
        pCamera.GetComponent<RetroCameraEffect>().enabled = true;
        Time.timeScale = 0f;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
