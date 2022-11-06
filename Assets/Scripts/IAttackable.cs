using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    float Health { get; }

    float Damage(float damage);
}
