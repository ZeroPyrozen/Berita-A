using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Canvas winCanvas;
    public Canvas loseCanvas;

    
    private float loseTimer;

    public int dimensionType;

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
        dimensionType = 1;
        loseTimer = 0f;
        Time.timeScale = 1;
	}
	
	void Update () {

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

}
