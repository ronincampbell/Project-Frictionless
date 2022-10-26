using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public Animator tapeSelector;

    public void tapeSelected()
    {
        tapeSelector.SetBool("IsSelected", true);
    }

    public void tapeUnselected()
    {
        tapeSelector.SetBool("IsSelected", false);
    }
}
