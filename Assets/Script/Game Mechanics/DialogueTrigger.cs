using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialog;
    private DialogManager dMan;
    public string[] dialogLines;
    public GameObject dialogBox;
   
    private void Start()
    {
        dMan = FindObjectOfType<DialogManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Feet")
        {
            Debug.Log("Masuk Dialog");
            if(!dMan.dialogueActive)
            {
                dMan.dialogueLines = dialogLines;
                dMan.currentLine = 0;
                dMan.ShowDialogue();
                dialogBox.SetActive(false);
            }
        }

    }

}
