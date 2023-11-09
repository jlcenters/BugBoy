using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hpBar; 



    public void SetHp(float hpNormalized)
    {
        hpBar.transform.localScale = new(hpNormalized, 1f);
    }

    public IEnumerator SlideHp(float newHp)
    {
        float currentHp = hpBar.transform.localScale.x;
        float changeAmount = currentHp - newHp;

        //begin moving hp bar to new hp until its difference reaches epsilon
        while (currentHp - newHp > Mathf.Epsilon)
        {
            currentHp -= changeAmount * Time.deltaTime;
            hpBar.transform.localScale = new(currentHp, 1f);
            yield return null;
        }

        //set hp to new to adjust remaining difference
        SetHp(newHp);
    }
}
