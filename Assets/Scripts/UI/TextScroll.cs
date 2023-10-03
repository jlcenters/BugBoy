using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TextScroll : MonoBehaviour
{
    private List<TextScrollSingle> textScrollSingles;
    private static TextScroll Instance;



    private void Awake()
    {
        textScrollSingles = new List<TextScrollSingle>();
        Instance = this;
    }
    private void Update()
    {
        for(int i = 0; i < textScrollSingles.Count; i++)
        {
            bool destroyInstance = textScrollSingles[i].ScrollText();

            if (destroyInstance)
            {
                textScrollSingles.RemoveAt(i);
                i--;
            }
        }
    }



    public static TextScrollSingle Init(TextMeshProUGUI dialogueObject, string dialogue, float secPerChar, bool invisibleChar, bool removePreviousInstance)
    {
        if (removePreviousInstance)
        {
            Instance.RemoveTextScroll(dialogueObject);
        }

        return Instance.AddToList(dialogueObject, dialogue, secPerChar, invisibleChar);
    }
    public TextScrollSingle AddToList(TextMeshProUGUI dialogueObject, string dialogue, float secPerChar, bool invisibleChar)
    {
        TextScrollSingle newTextScrollSingle = new(dialogueObject, dialogue, secPerChar, invisibleChar);
        textScrollSingles.Add(newTextScrollSingle);
        return newTextScrollSingle;
    }
    private void RemoveTextScroll(TextMeshProUGUI dialogueObject)
    {
        for (int i = 0; i < textScrollSingles.Count; i++)
        {
            if (textScrollSingles[i].GetDialogueObject() == dialogueObject)
            {
                textScrollSingles.RemoveAt(i);
                i--;
            }
        }
    }
}




public class TextScrollSingle
{
    private TextMeshProUGUI dialogueObject;
    private string dialogue;
    private int charIndex;
    private float secPerChar;
    private float timer;
    private bool invisibleChar;

    public TextScrollSingle(TextMeshProUGUI dialogueObject, string dialogue, float secPerChar, bool invisibleChar)
    {
        this.dialogueObject = dialogueObject;
        this.dialogue = dialogue;
        this.secPerChar = secPerChar;
        charIndex = 0;
        this.invisibleChar = invisibleChar;
    }



    public bool ScrollText()
    {
        if (dialogueObject != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0)
            {
                timer += secPerChar;
                charIndex++;

                string text = dialogue.Substring(0, charIndex);
                if (invisibleChar)
                {
                    text += "<color=#00000000>" + dialogue.Substring(charIndex) + "</color>";
                }

                if (charIndex > dialogue.Length)
                {
                    dialogue = null;
                    return true;
                }
                dialogueObject.text = text;
            }
        }
        return false;
    }
    public TextMeshProUGUI GetDialogueObject()
    {
        return dialogueObject;
    }
    public bool IsActive()
    {
        return charIndex < dialogueObject.text.Length;
    }
    public void ScrollTextAndDestroy()
    {

    }
}
