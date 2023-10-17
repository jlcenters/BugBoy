using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;
    [SerializeField] int statMultiplier;



    //structs that can change in battle
    public int Hp { get; set; }
    public int AttackPower {  get; set; }
    public int BlockPower {  get; set; }
    public MovementSpeed MovementSpeed { get; set; }
    public AttackSpeed AttackSpeed { get; set; }
    private void Update()
    {
        enemyBase.AttackPlayer(PlayerController.Instance);
    }


    //Constructing and Initializing enemy
    public Enemy(EnemyBase _base, int multiplier)
    {
        enemyBase = _base;
        statMultiplier = multiplier;
        
        Init();
    }
    public void Init()
    {
        Hp = enemyBase.MaxHp;
        AttackPower = enemyBase.AttackPower * statMultiplier;
        BlockPower = enemyBase.BlockPower * statMultiplier;
        MovementSpeed = enemyBase.MovementSpeed;
        AttackSpeed = enemyBase.AttackSpeed;
    }
}
