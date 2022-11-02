using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Quit the game when called (duh)
    public void OnClickExit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
