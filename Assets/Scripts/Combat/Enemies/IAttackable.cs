using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public void DamagePlayer(PlayerController player);

    public void ReceiveDamage(int damage);



}
