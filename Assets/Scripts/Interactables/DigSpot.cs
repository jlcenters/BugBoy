using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DigSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject wormPrefab;

    [Header("Must Equate to 100")]
    [SerializeField] private float wormProbability; //most common
    [SerializeField] private float honeyProbability;
    [SerializeField] private float flyProbability; //least common

    


    public void Interact(PlayerController player)
    {
        Debug.Log("dig spot interacted with!");
        //begin dig animation
        StartCoroutine(DigSequence());
    }



    private IEnumerator DigSequence()
    {
        //toggle dig 
        GameController.Instance.ToggleDig();
        //spawn 
        SpawnObject();
        yield return new WaitForSeconds(1f);
        //display what player received
        Debug.Log("blank blank captions!");
        yield return new WaitForSeconds(0.5f);
        //end dig and destroy object
        GameController.Instance.ToggleDig();
        Destroy(gameObject);

    }
    private void SpawnObject()
    {
        float rng = UnityEngine.Random.Range(0, 100);
        
        //TODO: add sprinkle instead of fly
        if(rng > flyProbability)
        {
            if(rng > honeyProbability)
            {
                SpawnWorm();
                Debug.Log("spawning worm");
            }
            else
            {
                SpawnHoney();
                Debug.Log("spawning honey");
            }
        }
        else
        {
            SpawnFly();
            Debug.Log("spawning fly");
        }
    }
    private void SpawnHoney()
    {
        float rng = UnityEngine.Random.Range(0, 100);
        //1/4 chance to completely fill inventory of honey 
        if(rng >= 75)
        {
            PlayerController.Instance.inventory.AddTool(ItemType.Honey, PlayerController.Instance.inventory.MAX_TOOLS);
        }
        else
        {
            PlayerController.Instance.inventory.AddTool(ItemType.Honey, 1);
        }
    }
    private void SpawnFly()
    {
        float rng = UnityEngine.Random.Range(0, 100);
        //25% chance to get a small bundle, 1% chance to get a large bundle
        if(rng == 100)
        {
            PlayerController.Instance.inventory.AddCollectable(CollectableType.Flies, 15);
        }
        else if(rng >= 75)
        {
            PlayerController.Instance.inventory.AddCollectable(CollectableType.Flies, 5);
        }
        else
        {
            PlayerController.Instance.inventory.AddCollectable(CollectableType.Flies, 1);
        }
    }
    private void SpawnWorm()
    {
        //instantiate prefab 
        Instantiate(wormPrefab, new(this.transform.position.x, 1f, this.transform.position.z), Quaternion.identity);
    }
}
