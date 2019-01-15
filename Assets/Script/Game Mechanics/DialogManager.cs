using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject DBox;
    public Text dText;
    public GameObject Player;
    private float speed;
    private float jumpForce;
    public bool dialogueActive;

    private bool playerStopped = false;

    public string[] dialogueLines;
    public int currentLine;

    public bool stopsPlayer = false;

    void Update()
    {
        if(dialogueActive&&playerStopped==false&&stopsPlayer)
        {
            //Stop player when he encounter something important
            speed = Player.GetComponent<PlayerMovement>().speed;
            jumpForce = Player.GetComponent<PlayerMovement>().jumpForce;
            Player.GetComponent<PlayerMovement>().speed = 0;
            Player.GetComponent<PlayerMovement>().jumpForce = 0;
            playerStopped = true;
        }
        if(dialogueActive&&(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.E)))
        {
            //Next dialogue
            currentLine++;
        }
        if(currentLine >= dialogueLines.Length)
        {
            //Resetting player movement and make dialog box disappear
            DBox.SetActive(false);
            dialogueActive = false;
            currentLine = 0;
            Player.GetComponent<PlayerMovement>().speed = speed;
            Player.GetComponent<PlayerMovement>().jumpForce = jumpForce;
            playerStopped = false;
        }
        dText.text = dialogueLines[currentLine];
    }
    public void ShowBox(string dialogue)
    {
        Debug.Log("Dialog Box Muncul");
        dialogueActive = true;
        DBox.SetActive(true);
        dText.text = dialogue;
    }
    public void ShowDialogue(bool stopsPlayer)
    {
        this.stopsPlayer = stopsPlayer;
        Debug.Log("Dialog Muncul");
        dialogueActive = true;
        DBox.SetActive(true);
    }
}

