using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//These items will be added up at the end of the game to help with total score
//flies can also be used as currency
//sprinkles are used to get to the good ending
public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] private int value = 1;

    public CollectableType type;



    public void Interact(PlayerController player)
    {
        player.inventory.AddCollectable(type, value);
        Destroy(gameObject);
    }
}



public enum CollectableType
{
    None,
    Flies,
    Tokens,
    Badges,
    Sprinkles
}
