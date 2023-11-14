using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour, IInteractable
{
    [SerializeField] private HatType type;

    public void Interact(PlayerController player)
    {
        //sets player bool of hat type to true
        player.inventory.AddHat(type);
        //destroys object
        Destroy(gameObject);
    }

}


//
public enum HatType
{
    None,
    Bee,
    Ant,
    Beetle,
    Mosquito
}
