using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float lettersPerSecond;
    private int currentLine = 0;
    private Dialogue dialogue;
    public bool IsTyping { get; private set; }

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;



    private void Awake()
    {
        Instance = this;
    }
    


    public void InitDialogue(GameObject newDialogueBox, TMP_Text newDialogueText, Dialogue newDialogue)
    {
        dialogueBox = newDialogueBox;
        dialogueText = newDialogueText;
        currentLine = 0;
        dialogue = newDialogue;
    }
    public IEnumerator ShowDialogue()
    {
        yield return new WaitForEndOfFrame();
        //OnShowDialogue?.Invoke();
        //dialogueBox.SetActive(true);
        yield return TypeDialogue(dialogue.DialogueLines[0]);
    }
    public IEnumerator TypeDialogue(string lineToType)
    {
        IsTyping = true;
        dialogueText.text = "";
        foreach(var letter in lineToType.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        IsTyping = false;
        yield return new WaitForSeconds(1f);
    }
    public void TryNextLine()
    {
        currentLine++;
        if (currentLine < dialogue.DialogueLines.Count)
        {
            StartCoroutine(TypeDialogue(dialogue.DialogueLines[currentLine]));
        }
        else
        {
            currentLine = 0;
            dialogueBox.SetActive(false);
            //OnCloseDialogue?.Invoke();
        }
    }
}



[System.Serializable]
public class Dialogue
{
    [SerializeField] private List<string> dialogueLines;

    public List<string> DialogueLines => dialogueLines;
}
