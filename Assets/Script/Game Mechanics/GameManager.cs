using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Canvas winCanvas;
    public Canvas loseCanvas;
    public Canvas pauseCanvas;
    
    private float loseTimer;

    public int dimensionType;

    public static bool pauseChecker;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start () {
        pauseChecker = false;
        dimensionType = 1;
        loseTimer = 0f;
        Time.timeScale = 1;
	}
	
	void Update () {
        if (Input.GetKeyDown("escape") || pauseChecker)
        {
            TogglePause();
            pauseChecker = false;
        }
    }

    public void RoundEnd(bool win)
    {
        
        if (win)
        {
            winCanvas.gameObject.SetActive(true);
        }
        else
        {
            //loseTimer = 1.5f;
            //while (loseTimer > 0)
            //{
            //    loseTimer -= Time.deltaTime;
            //}
            
            loseCanvas.gameObject.SetActive(true);
            
        }
        Time.timeScale = 0;
    }

    private void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseCanvas.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseCanvas.gameObject.SetActive(false);
        }
    }

}
