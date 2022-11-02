using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    // Declare necessary variables
    public Animator tapeSelector;

    // Change animator variables on select and unselect
    public void tapeSelected()
    {
        tapeSelector.SetBool("IsSelected", true);
    }

    public void tapeUnselected()
    {
        tapeSelector.SetBool("IsSelected", false);
    }
}
