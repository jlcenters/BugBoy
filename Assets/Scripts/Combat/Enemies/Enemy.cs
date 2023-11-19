using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] private EnemyBase enemyBase;
    [SerializeField] int statMultiplier;



    //structs that can change in battle
    public int Hp { get; set; }
    public int AttackPower {  get; private set; }
    public int BlockPower {  get; private set; }
    public float MovementSpeed { get; private set; }
    public float AttackSpeed { get; private set; }



    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
    }
    private void Start()
    {
        Init();
    }
    


    public void Init()
    {
        Hp = enemyBase.MaxHp;
        AttackPower = enemyBase.AttackPower * statMultiplier;
        BlockPower = enemyBase.BlockPower * statMultiplier;
        MovementSpeed = enemyBase.CalculateSpeed(enemyBase.MovementSpeed);
        AttackSpeed = enemyBase.CalculateSpeed(enemyBase.AttackSpeed);
    }



    //IAttackable Methods
    public void DamagePlayer(PlayerController player)
    {
        player.TakeDamage(AttackPower);
    }
    public void ReceiveDamage(int damage)
    {
        damage -= BlockPower;
        if(damage <= 0)
        {
            damage = 1;
            Debug.Log("block too high or damage too low");
        }
        Hp -= damage;
        if(Hp <= 0)
        {
            Debug.Log("dead");
            Destroy(gameObject);
        }
    }
}
