using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class HatButtonUI : MonoBehaviour
{
    [SerializeField] private HatType hatType;
    [SerializeField] private TextMeshProUGUI text;


    public void SetUpButton(HatType hat, string buttonText)
    {
        hatType = hat;
        SetButtonText(buttonText);

        GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerController.Instance.inventory.SetActiveHat(hatType);
        }); //add on click listener to change button to type
    }
    public void SetButtonText(string buttonText)
    {
        text.text = buttonText;
        GetComponentInChildren<TMP_Text>().text = buttonText; //set text of button

    }
}
