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
            Debug.Log("player attempting to interact");
            Collider[] collidedObjects = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collidedObject in collidedObjects)
            {
                Debug.Log("checking collided object");
                if(collidedObject.TryGetComponent(out Talkable talkableNPC)){
                    Debug.Log("interacting w npc");
                    talkableNPC.Interact(GetComponent<PlayerController>());
                }
            }
        }
    }
}
