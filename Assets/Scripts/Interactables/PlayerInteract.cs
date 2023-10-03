using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] collidedObjects = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collidedObject in collidedObjects)
            {
                if(collidedObject.TryGetComponent(out Talkable talkableNPC)){
                    talkableNPC.Interact(GetComponent<PlayerController>());
                }
            }
        }
    }
}
