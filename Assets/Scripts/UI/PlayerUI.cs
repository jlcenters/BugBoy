using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
    [SerializeField] private HpBarUI hpBar;
    //TODO: held hat and item



    private void Start()
    {
        SetData();
    }



    private void SetData()
    {
        SetHp();
        //reset hat option
        //reset item option
    }
    private void SetHp()
    {
        hpBar.SetHp((float)PlayerController.Instance.Hp / PlayerController.Instance.MaxHp);
    }
    public IEnumerator UpdateHp()
    {
        yield return hpBar.SlideHp((float)PlayerController.Instance.Hp / PlayerController.Instance.MaxHp);
    }
}
