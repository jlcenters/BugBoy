using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: create scriptable object
public class Tool : MonoBehaviour, IInteractable
{
    [SerializeField] private int value = 1;
    public ItemType type;



    public void Interact(PlayerController player)
    {
        //loop the entire value of the item, descending
        for(int i = value; i > 0; i--)
        {
            //if the current value added to inventory is at most 10, add i objects to inventory and return
            if (player.inventory.Honey + i <= player.inventory.MAX_TOOLS)
            {
                player.inventory.AddTool(type, i);
                player.playerUi.SetItem();
                Destroy(gameObject);
                return;
            }
        }
        //if could not add any value of objects to inventory, leave object
        return;
    }
}



public enum ItemType
{
    Honey,
    Ants,
    Mud
}
