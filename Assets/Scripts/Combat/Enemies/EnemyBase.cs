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
    [SerializeField] private MovementSpeed movementSpeed;
    [SerializeField] private AttackSpeed attackSpeed;



    public string EnemyName => enemyName;
    public string Description => description;
    public PrefabStorage EnemyPrefab => enemyPrefab;
    public EnemyType EnemyType => enemyType;
    public int MaxHp => maxHp;
    public int AttackPower => attackPower;
    public int BlockPower => blockPower;
    public MovementSpeed MovementSpeed => movementSpeed;
    public AttackSpeed AttackSpeed => attackSpeed;



    public float GetAttackSpeed(AttackSpeed speed)
    {
        float calculatedSpeed = 0f;

        if(attackSpeed == AttackSpeed.verySlow)
        {
            calculatedSpeed = 3f;
        }
        else if(attackSpeed == AttackSpeed.slow)
        {
            calculatedSpeed = 2f;
        }
        else if(attackSpeed == AttackSpeed.average)
        {
            calculatedSpeed = 1f;
        }
        else if(attackSpeed == AttackSpeed.fast)
        {
            calculatedSpeed = 0.75f;
        }
        else if(attackSpeed == AttackSpeed.veryFast)
        {
            calculatedSpeed = 0.5f;
        }


        return calculatedSpeed;
    }
}



public enum AttackSpeed
{
    verySlow, slow, average, fast, veryFast
}



public enum MovementSpeed
{
    verySlow, slow, average, fast, veryFast
}



public enum EnemyType
{
    worm, mosquito, spider, spiderBaby
}