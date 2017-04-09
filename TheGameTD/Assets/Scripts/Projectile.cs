using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum proType
{
    arrow, rock, fireball
};

public class Projectile : MonoBehaviour {

    [SerializeField]
    private int attackStrength;
    [SerializeField]
    private proType projectileType;

    public int AttackStrength
    {
        get
        {
            return attackStrength;
        }
    }

    public proType ProjectileType
    {
        get
        {
            return projectileType;
        }
    }
}
