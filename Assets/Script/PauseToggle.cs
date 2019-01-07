using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public void TogglePause()
    {
        GameManager.pauseChecker = true;
    }
}
