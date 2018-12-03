using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : MonoBehaviour
{
    public WeaponType weaponType;
    public enum WeaponType
    {
        Slash = 0,
        Crush = 1,
        None = 3

    }

    public int damage;
}
