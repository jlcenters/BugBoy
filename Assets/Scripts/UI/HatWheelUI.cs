using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HatWheelUI : MonoBehaviour
{
    private List<HatType> playerHats = new();
    //[SerializeField] private Button[] buttons = new Button[4];
    [SerializeField] private HatButtonUI[] btns = new HatButtonUI[4];
    /*[SerializeField] private TextMeshProUGUI hat0Text;
    [SerializeField] private TextMeshProUGUI hat1Text;
    [SerializeField] private TextMeshProUGUI hat2Text;
    [SerializeField] private TextMeshProUGUI hat3Text;*/



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
        btns[0].SetUpButton(playerHats[0], "No Hat");
        for(int i = 1; i < btns.Length; i++)
        {
            if (playerHats.Count > i)
            {
                btns[i].SetUpButton(playerHats[i], playerHats[i].ToString() + "Hat");
            }
            else
            {
                btns[i].SetButtonText("-Empty-");
            }
        }
    }
    public void SetUpWheel()
    {
        GetHats();
        SetDisplay();
    }
}
