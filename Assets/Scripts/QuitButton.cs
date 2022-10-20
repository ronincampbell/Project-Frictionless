using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnClickExit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
