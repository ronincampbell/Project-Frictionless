using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCursor : MonoBehaviour
{

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Disable cursor after confirming button press
    public void dCursor ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
