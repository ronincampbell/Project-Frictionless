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
    public GameObject musicController;
    public AudioSource audioSource;
    public AudioClip deathSFX;
    public float volume = 1f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ResetGame();
        }
    }
    public void PlayerDie()
    {
        audioSource.PlayOneShot(deathSFX, volume);
        DeathScreen.SetActive(true);
        AliveUI.SetActive(false);
        musicController.SetActive(false);

        pCamera.GetComponent<RetroCameraEffect>().enabled = true;
        Time.timeScale = 0f;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
