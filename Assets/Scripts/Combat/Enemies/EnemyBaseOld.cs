using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * 
 * NOT CURRENTLY USING SCRIPTABLE OBJECT; USING ENEMYBASE INSTEAD
 * 
 * 
 */
[CreateAssetMenu(fileName = "EnemyBase", menuName = "Enemy/Create New Enemy")]
public class EnemyBaseOld : ScriptableObject
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



   /*public void AttackPlayer(PlayerController playerController)
    {
        playerController.hp -= 1;
    }*/
}


/*
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
*/