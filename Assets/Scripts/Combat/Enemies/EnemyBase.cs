using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private PrefabStorage enemyPrefab;
    [SerializeField] private EnemyType enemyType;

    [Header("Base Stats")]
    [SerializeField] private int maxHp;
    [SerializeField] private int attackPower;
    [SerializeField] private int blockPower;
    [SerializeField] private Speed movementSpeed;
    [SerializeField] private Speed attackSpeed;

    [Header("Movement Speeds for Testing")]
    [SerializeField] private float verySlow;
    [SerializeField] private float slow;
    [SerializeField] private float average;
    [SerializeField] private float fast;
    [SerializeField] private float veryFast;




    public string EnemyName => enemyName;
    public string Description => description;
    public PrefabStorage EnemyPrefab => enemyPrefab;
    public EnemyType EnemyType => enemyType;
    public int MaxHp => maxHp;
    public int AttackPower => attackPower;
    public int BlockPower => blockPower;
    public Speed MovementSpeed => movementSpeed;
    public Speed AttackSpeed => attackSpeed;



    public float CalculateSpeed(Speed speed, string type)
    {
        switch (type)
        {
            case Enemy.MOVEMENT_TXT:
                switch (speed)
                {
                    case Speed.verySlow:
                        return verySlow;
                    case Speed.slow:
                        return slow;
                    case Speed.average:
                        return average;
                    case Speed.fast:
                        return fast;
                    case Speed.veryFast:
                        return veryFast;
                }
                break;
            case Enemy.ATTACK_TXT:
                switch (speed)
                {
                    case Speed.verySlow:
                        return 3f;
                    case Speed.slow:
                        return 2f;
                    case Speed.average:
                        return 1f;
                    case Speed.fast:
                        return 0.75f;
                    case Speed.veryFast:
                        return 0.5f;
                }
                break;
        }
        return 0f;
    }
}



public enum Speed
{
    verySlow, slow, average, fast, veryFast
}



public enum EnemyType
{
    worm, mosquito, spider, spiderBaby
}
