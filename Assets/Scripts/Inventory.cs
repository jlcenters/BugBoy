using System.Collections;
using System.Collections.Generic;
using System.Net;
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
    public readonly int MAX_TOOLS = 5;

    [Header("Collectables")]
    [SerializeField] private int tokens;
    [SerializeField] private int badges;
    [SerializeField] private int sprinkles;

    [Header("Hats")]
    [SerializeField] private bool antHat = false;
    [SerializeField] private bool beeHat = false;
    [SerializeField] private bool beetleHat = false;
    [SerializeField] private bool mosquitoHat = false;

    //ant = speed, bee = attack, beetle = defense
    [Header("Hat Buffs")]
    public HatType activeHat = HatType.None;
    public readonly int ANT_BUFF = 5;
    public readonly int BEE_BUFF;
    public readonly int BEETLE_BUFF;

    public Dictionary<CollectableType, int> collectables = new();
    public Dictionary<ItemType, int> tools = new();
    public Dictionary<HatType, bool> hats = new();
    //TODO: active tool

    /*public int Flies { get { return flies; } private set { flies = value; } }
    public int Tokens { get { return tokens; } set { tokens = value; } }
    public int Badges { get { return badges; } set { badges = value; } }
    public int Sprinkles { get { return sprinkles; } set { sprinkles = value; } }
    */
    public int Honey { get { return honey; } set { honey = value; } }

    private void Awake()
    {
        collectables.Add(CollectableType.Flies, flies);
        collectables.Add(CollectableType.Tokens, tokens);
        collectables.Add(CollectableType.Badges, badges);
        collectables.Add(CollectableType.Sprinkles, sprinkles);

        tools.Add(ItemType.Honey, honey);
        tools.Add(ItemType.Ants, ants);
        tools.Add(ItemType.Mud, mud);

        hats.Add(HatType.None, true);
        hats.Add(HatType.Ant, antHat);
        hats.Add(HatType.Bee, beeHat);
        hats.Add (HatType.Beetle, beetleHat);
        hats.Add(HatType.Mosquito, mosquitoHat);
    }



    public void AddCollectable(CollectableType key, int value)
    {
        collectables[key] += value;
        UpdateCollectableInventory();
    }
    public void AddTool(ItemType key, int value)
    {
        if (tools[key] + value > MAX_TOOLS)
        {
            tools[key] = MAX_TOOLS;
        }
        else
        {
            tools[key] += value;
        }
        UpdateToolInventory();
    }
    public void AddHat(HatType key)
    {
        hats[key] = true;
        UpdateHatInventory();
        SetActiveHat(key);
    }



    public bool ConsumeTool(ItemType itemType, int amountUsed)
    {
        if (tools[itemType] - amountUsed < 0)
        {
            return false;
        }

        tools[itemType] -= amountUsed;
        UpdateToolInventory();
        return true;
    }
    public void SetActiveHat(HatType hatType)
    {
        activeHat = hatType;
    }



    private void UpdateCollectableInventory()
    {
        flies = collectables[CollectableType.Flies];
        tokens = collectables[CollectableType.Tokens];
        badges = collectables[CollectableType.Badges];
        sprinkles = collectables[CollectableType.Sprinkles];
    }
    private void UpdateToolInventory()
    {
        honey = tools[ItemType.Honey];
        ants = tools[ItemType.Ants];
        mud = tools[ItemType.Mud];
    }
    private void UpdateHatInventory()
    {
        antHat = hats[HatType.Ant];
        beeHat = hats[HatType.Bee];
        beetleHat = hats[HatType.Beetle];
        mosquitoHat = hats[HatType.Mosquito];
    }
}
