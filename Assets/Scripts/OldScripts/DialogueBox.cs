using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
/*
 * 
 * 
 * 
 * SCRIPT OBSOLETE
 * 
 * 
 * 
 */
public class DialogueBox : MonoBehaviour
{
    private static DialogueBox Instance;
    [SerializeField] private TextMeshPro dialogueTxt;
    [SerializeField] private Text dObject;
    [SerializeField] private SpriteRenderer dialogueBoxBG;
    [SerializeField] private float padding;
    [SerializeField] private TextScrollSingle textScrollSingle;
    [SerializeField] private GameObject initialDialogueBox;



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
        //dialogueTxt = GetComponentInChildren<TextMeshPro>();
        dObject = GetComponentInChildren<Text>();
        dialogueBoxBG = GetComponentInChildren<SpriteRenderer>();
        Setup("E");
    }



    public static void Talk(Transform parent, Vector3 localPosition, string text)
    {
        Transform dialogueBoxPosition = Instantiate(PrefabStorage.Instance.dialogueBoxPrefab, parent);
        dialogueBoxPosition.localPosition = localPosition;

        if (Instance.textScrollSingle != null && Instance.textScrollSingle.IsActive())
        {
            Debug.Log("textscroll wasn't null and instance was active");

            Instance.textScrollSingle.ScrollTextAndDestroy();
        }
        else
        {
            Debug.Log("textscroll was null or instance was inactive");

            dialogueBoxPosition.GetComponent<DialogueBox>().Setup(text);
            Instance.textScrollSingle = TextScroll.Init(Instance.dObject/*dialogueObject*/, text, 20f, true, false);
            Instance.Remove();
        }
    }
    private void Setup(string dialogue)
    {
        dialogueTxt.text = dialogue;
        dialogueTxt.ForceMeshUpdate();

        Vector2 textSize = dialogueTxt.GetRenderedValues(false);
        Vector2 bgPadding = new(padding, padding);

        dialogueBoxBG.size = textSize + bgPadding;
    }
    private void Remove()
    {
        Destroy(this.gameObject, 4f);
    }
}
