using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject DBox;
    public Text dText;

    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentLine;

    void Update()
    {
        if(dialogueActive&&(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.E)))
        {
            currentLine++;
        }
        if(currentLine >= dialogueLines.Length)
        {
            DBox.SetActive(false);
            dialogueActive = false;
            currentLine = 0;
        }
        dText.text = dialogueLines[currentLine];
    }
    public void ShowBox(string dialogue)
    {
        Debug.Log("Box Muncul");
        dialogueActive = true;
        DBox.SetActive(true);
        dText.text = dialogue;
    }
    public void ShowDialogue()
    {
        Debug.Log("Dialog Muncul");
        dialogueActive = true;
        DBox.SetActive(true);
    }
}

