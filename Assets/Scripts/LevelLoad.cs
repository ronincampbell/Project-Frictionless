using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{

    //
    // DEPECATED DO NOT USE | DEPECATED DO NOT USE | DEPECATED DO NOT USE | DEPECATED DO NOT USE | DEPECATED DO NOT USE | 
    //
    public int levelnumber;

    private int highestlevel;

    private void Start() 
    {
        highestlevel = PlayerPrefs.GetInt("UnlockedLevels");
    }

    private void OnTriggerEnter(Collider other)
    {
        UnlockLevel();
        Debug.Log("Loading next scene...");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    private void UnlockLevel()
    {
        if (highestlevel < levelnumber)
         {
            PlayerPrefs.SetInt("UnlockedLevels", levelnumber);
            Debug.Log(PlayerPrefs.GetInt("UnlockedLevels"));
         }
    }
}
