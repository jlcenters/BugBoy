using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class DialogueBox : MonoBehaviour
{
    private static DialogueBox Instance;
    [SerializeField] private TextMeshPro dialogueTxt;
    [SerializeField] private TextMeshProUGUI dialogueObject;
    [SerializeField] private SpriteRenderer dialogueBoxBG;
    [SerializeField] private float padding;
    [SerializeField] private TextScrollSingle textScrollSingle;
    [SerializeField] private float typingSpeed;



    private void Awake()
    {
        Instance = this;
        dialogueTxt = GetComponentInChildren<TextMeshPro>();
        dialogueBoxBG = GetComponentInChildren<SpriteRenderer>();

        Setup("E");
    }



    public static void Talk(Transform parent, Vector3 localPosition, string text)
    {
        Transform dialogueBoxPosition = Instantiate(PrefabStorage.Instance.dialogueBoxPrefab, parent);
        dialogueBoxPosition.localPosition = localPosition;

        if (Instance.textScrollSingle != null && Instance.textScrollSingle.IsActive())
        {

        }
        else
        {
            dialogueBoxPosition.GetComponent<DialogueBox>().Setup(text);
            Instance.textScrollSingle = TextScroll.Init(Instance.dialogueObject, text, Instance.typingSpeed, true, false);
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
