using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HatWheelUI : MonoBehaviour
{
    private List<HatType> playerHats = new();
    [SerializeField] private Button[] buttons = new Button[4];
    [SerializeField] private TextMeshProUGUI hat0Text;
    [SerializeField] private TextMeshProUGUI hat1Text;
    [SerializeField] private TextMeshProUGUI hat2Text;
    [SerializeField] private TextMeshProUGUI hat3Text;



    private void Start()
    {
        SetUpWheel();
    }



    private void GetHats()
    {
        List<HatType> hatsFromInventory = new()
        {
            HatType.None
        };

        if (PlayerController.Instance.inventory.hats[HatType.Ant] == true )
        {
            hatsFromInventory.Add(HatType.Ant);
        }
        if (PlayerController.Instance.inventory.hats[HatType.Bee] == true )
        {
            hatsFromInventory.Add(HatType.Bee);
        }
        if (PlayerController.Instance.inventory.hats[HatType.Beetle] == true )
        {
            hatsFromInventory.Add(HatType.Beetle);
        }

        playerHats = hatsFromInventory;
    }
    private void SetDisplay()
    {
        buttons[0].GetComponentInChildren<TMP_Text>().text = playerHats[0].ToString();
        for(int i = 0; i < buttons.Length; i++)
        {
            if (playerHats.Count > i)
            {
                buttons[i].GetComponentInChildren<TMP_Text>().text = "hello " + playerHats[i].ToString();
            }
            else
            {
                buttons[i].GetComponentInChildren<TMP_Text>().text = "-Empty-";
            }
        }
        //hat0Button.GetComponentInChildren<TMP_Text>().text = "hello";

    }
    public void SetUpWheel()
    {
        GetHats();
        SetDisplay();
    }

}
