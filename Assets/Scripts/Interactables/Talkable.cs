using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour, IInteractable
{
    //[SerializeField] private string text;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private GameObject initialBubble;
    [SerializeField] private int dialogueLine = 0;


    public void Interact(PlayerController player)
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
    }
}
