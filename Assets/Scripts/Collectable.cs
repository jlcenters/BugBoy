using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        Debug.Log("interacted");
        //Dictionary<string, int> inventory = new(player.GetComponent<Inventory>().)
        Destroy(gameObject);
    }
}
