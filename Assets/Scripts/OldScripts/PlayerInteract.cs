using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 * 
 * 
 * 
 * SCRIPT OBSOLETE; CHECK PLAYER CONTROLLER FOR NEW IMPLEMENTATION
 * 
 * 
 * 
 * 
 */
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask interactablesLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("player attempting to interact");
            Collider[] interactables = Physics.OverlapSphere(transform.position, interactRange, interactablesLayer);
            if(interactables != null)
            {
                for(int i = 0; i < interactables.Length; i++)
                {
                    IInteractable interactable = interactables[i].GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact(this.gameObject.GetComponent<PlayerController>());
                        break;
                    }
                }
            }
        }
    }
}
