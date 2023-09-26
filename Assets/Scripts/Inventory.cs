using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Dictionary<string, int> inventory = new();

    public int Flies => flies;
    public int Ants => ants;
    public int Mud => mud;
    public int Honey => honey;
    public int Tokens => tokens;
    public int Badges => badges;
    public int Sprinkles => sprinkles;


    private void Awake()
    {
        inventory.Add("flies", Flies);
        inventory.Add("ants", Ants);
        inventory.Add("mud", Mud);
        inventory.Add("honey", Honey);
        inventory.Add("tokens", Tokens);
        inventory.Add("badges", Badges);
        inventory.Add("sprinkles", Sprinkles);
    }
    //getters and setters?
    //method to check amt
    //method to adjust amount
}
