using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using RetroAesthetics;

public class DeathScript : MonoBehaviour
{
    // Declare necessary variables
    public GameObject pCamera;
    public GameObject DeathScreen;
    public GameObject AliveUI;
    public GameObject musicController;
    public AudioSource audioSource;
    public AudioClip deathSFX;
    public float volume = 1f;

    private void Update()
    {
        // Call reset function when F key is pressed
        if(Input.GetKeyDown(KeyCode.F))
        {
            ResetGame();
        }
    }
    // When player dies, freeze time and display death UI
    public void PlayerDie()
    {
        audioSource.PlayOneShot(deathSFX, volume);
        DeathScreen.SetActive(true);
        AliveUI.SetActive(false);
        musicController.SetActive(false);
        pCamera.GetComponent<RetroCameraEffect>().enabled = true;
        Time.timeScale = 0f;
    }


    // Set time back to normal and reload the scene
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
