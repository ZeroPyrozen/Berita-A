using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasToggler : MonoBehaviour {

    public Canvas toggleCanvas;
    [SerializeField]private static bool isOpen = false;

	
	
	public void OnClick () {
           Toggle();
    }

    private void Toggle()
    {
        if(!isOpen)
        {
            toggleCanvas.gameObject.SetActive(true);
            isOpen = true;
        }
        else if(isOpen) 
        {
            toggleCanvas.gameObject.SetActive(false);
            isOpen = false;
        }
    }
}
