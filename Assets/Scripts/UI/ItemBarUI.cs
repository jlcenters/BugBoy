using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class ItemBarUI : MonoBehaviour
{
    [SerializeField] private GameObject itemBar;
    //max amount of each item
    private const int MAX_ITEMS = 5;


    public void SetItem(float itemsInInventory)
    {
        itemBar.transform.localScale = new(1f, itemsInInventory / MAX_ITEMS);
    }

    /*public IEnumerator SlideItemBar(float newItemAmount)
    {
        float currentAmount = itemBar.transform.localScale.x;
        float changeAmount = currentAmount - newItemAmount;

        //begin moving Item bar to new amount until its difference reaches epsilon
        while (currentAmount - newItemAmount > Mathf.Epsilon)
        {
            currentAmount -= changeAmount * Time.deltaTime;
            itemBar.transform.localScale = new(1f, currentAmount);
            yield return null;
        }

        //set bar to new to adjust remaining difference
        SetItem(newItemAmount);
    }*/
}
