using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * 
 * 
 * SCRIPT OBSOLETE
 * 
 * 
 * 
 * 
 */
public class PrefabStorage : MonoBehaviour
{
    private static PrefabStorage _i;
    public static PrefabStorage Instance 
    {  
        get 
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<PrefabStorage>("PrefabStorage")).GetComponent<PrefabStorage>();
            }
            return _i; 
        } 
    }

    public Transform dialogueBoxPrefab;
}
