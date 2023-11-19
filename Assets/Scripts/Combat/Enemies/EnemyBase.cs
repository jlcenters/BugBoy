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



    public string EnemyName => enemyName;
    public string Description => description;
    public PrefabStorage EnemyPrefab => enemyPrefab;
    public EnemyType EnemyType => enemyType;
    public int MaxHp => maxHp;
    public int AttackPower => attackPower;
    public int BlockPower => blockPower;
    public Speed MovementSpeed => movementSpeed;
    public Speed AttackSpeed => attackSpeed;



    public float CalculateSpeed(Speed speed)
    {
        float calculatedSpeed = 0f;

        if(attackSpeed == Speed.verySlow)
        {
            calculatedSpeed = 3f;
        }
        else if(attackSpeed == Speed.slow)
        {
            calculatedSpeed = 2f;
        }
        else if(attackSpeed == Speed.average)
        {
            calculatedSpeed = 1f;
        }
        else if(attackSpeed == Speed.fast)
        {
            calculatedSpeed = 0.75f;
        }
        else if(attackSpeed == Speed.veryFast)
        {
            calculatedSpeed = 0.5f;
        }


        return calculatedSpeed;
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
