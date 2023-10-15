using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Talkable : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;    



    public void Interact(PlayerController player)
    {
        DialogueController.Instance.InitDialogue(dialogueBox, dialogueText, dialogue);
        StartCoroutine(DialogueController.Instance.ShowDialogue());
    }



    /*public void Interact(PlayerController player)
    {
        if(initialBubble.activeInHierarchy)
        {
            Debug.Log("disabling initial dialogue bubble");
            initialBubble.SetActive(false);
        }
        if(dialogueLine < dialogueLines.Length)
        {
            Debug.Log("calling talk function");
            DialogueBox.Talk(this.transform, new(0f, 1.5f), dialogueLines[dialogueLine]);
            dialogueLine++;
            if(dialogueLine >= dialogueLines.Length)
            {
                dialogueLine = 0;
            }
        }
    }*/
}
