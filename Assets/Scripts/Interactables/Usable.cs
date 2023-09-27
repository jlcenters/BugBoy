using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: create scriptable object
public class Usable : MonoBehaviour, IInteractable
{
    [SerializeField] private int value = 1;

    public ItemType type;



    public void Interact(PlayerController player)
    {
        for (int i = 0; i < value; i++)
        {
            player.inventory.tools.Add(this);
        }
        Destroy(gameObject);
    }
}



public enum ItemType
{
    Honey,
    Ants,
    Mud
}
