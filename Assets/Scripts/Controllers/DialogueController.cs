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
    [SerializeField] private float padding;
    public Color transparentColor;
    public Color normalColor;
    private TMP_Text dialogueText;
    private float lettersPerSecond;
    private int currentLine = 0;
    private Dialogue dialogue;
    public bool IsTyping { get; private set; }

    public event Action OnDialogue;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    


    public void InitDialogue(GameObject newDialogueBox, TMP_Text newDialogueText, Dialogue newDialogue, float newLettersPerSecond)
    {
        dialogueBox = newDialogueBox;
        dialogueText = newDialogueText;
        currentLine = 0;
        lettersPerSecond = newLettersPerSecond;
        dialogue = newDialogue;
    }
    public IEnumerator ShowDialogue()
    {
        yield return new WaitForEndOfFrame();
        OnDialogue?.Invoke();
        yield return TypeDialogue(dialogue.DialogueLines[currentLine]);
    }
    public IEnumerator TypeDialogue(string lineToType)
    {
        IsTyping = true;
        SetDialogueBoxSize(lineToType);
        dialogueText.text = "";
        foreach (var letter in lineToType.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        yield return new WaitForSeconds(0.25f);
        IsTyping = false;

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
            ResetDialogueBox();
            GameController.Instance.ToggleDialogue();
        }
    }
    private void SetDialogueBoxSize(string lineToFit)
    {
        dialogueText.text = lineToFit;
        dialogueText.ForceMeshUpdate();

        Vector2 lineSize = dialogueText.GetRenderedValues(false);
        Vector2 boxPadding = new(padding, padding);

        dialogueBox.GetComponentInChildren<SpriteRenderer>().size = lineSize + boxPadding;
    }
    private void ResetDialogueBox()
    {
        currentLine = 0;
        dialogueText.text = "E";
        SetDialogueBoxSize("E");
    }
}



[System.Serializable]
public class Dialogue
{
    [TextArea]
    [SerializeField] private List<string> dialogueLines;

    public List<string> DialogueLines => dialogueLines;
}
