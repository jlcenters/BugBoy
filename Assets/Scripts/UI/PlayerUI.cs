using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
    [SerializeField] private HpBarUI hpBar;
    [SerializeField] private ItemBarUI itemBar;
    //TODO: held hat



    private void Start()
    {
        SetData();
    }



    private void SetData()
    {
        SetHp();
        //reset hat option
        SetItem();
    }
    public void SetHp()
    {
        hpBar.SetHp((float)PlayerController.Instance.Hp / PlayerController.Instance.MaxHp);
    }
    public void SetItem()
    {
        itemBar.SetItem((float)PlayerController.Instance.inventory.Honey);
    }



    public IEnumerator UpdateHp()
    {
        yield return hpBar.SlideHp((float)PlayerController.Instance.Hp / PlayerController.Instance.MaxHp);
    }
    /*public IEnumerator UpdateItem()
    {
        yield return itemBar.SlideItemBar((float)PlayerController.Instance.inventory.Honey);
    }*/

}
