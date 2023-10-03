using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour, IInteractable
{
    //[SerializeField] private string text;
    [SerializeField] private string[] dialogueLines;



    public void Interact(PlayerController player)
    {
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            DialogueBox.Talk(this.transform, new(0f, 1.5f), dialogueLines[i]);
        }
    }
}
