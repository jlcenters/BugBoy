using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



//TODO: create scriptable object
public class Inventory : MonoBehaviour
{
    [Header("Currency")]
    [SerializeField] private int flies;

    [Header("Tools")]
    [SerializeField] private int ants;
    [SerializeField] private int mud;
    [SerializeField] private int honey;

    [Header("Collectables")]
    [SerializeField] private int tokens;
    [SerializeField] private int badges;
    [SerializeField] private int sprinkles;

    public Dictionary<CollectableType, int> collectables = new();
    public List<Usable> tools = new();
    public List<Hat> hats = new();

    public int Flies { get { return flies; } set { flies = value; } }
    public int Tokens { get { return tokens; } set { tokens = value; } }
    public int Badges { get { return badges; } set { badges = value; } }
    public int Sprinkles { get { return sprinkles; } set { sprinkles = value; } }



    private void Awake()
    {
        collectables.Add(CollectableType.Flies, Flies);
        collectables.Add(CollectableType.Tokens, Tokens);
        collectables.Add(CollectableType.Badges, Badges);
        collectables.Add(CollectableType.Sprinkles, Sprinkles);
    }



    public void UpdateCollectable(CollectableType key, int value)
    {
        collectables[key] += value;
        UpdateCollectableInventory();
    }

    public void UseTool(ItemType itemType)
    {
        //buff/debuff

        //consume tool
        for (int i = 0; i < tools.Count; i++)
        {
            if(tools[i].type == itemType)
            {
                tools.Remove(tools[i]);
                break;  
            }
        }
    }



    private void UpdateCollectableInventory()
    {
        Flies = collectables[CollectableType.Flies];
        Tokens = collectables[CollectableType.Tokens];
        Badges = collectables[CollectableType.Badges];
        Sprinkles = collectables[CollectableType.Sprinkles];
    }
}
