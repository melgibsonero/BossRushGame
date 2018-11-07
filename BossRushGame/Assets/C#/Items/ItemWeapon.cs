using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : MonoBehaviour
{
    public WeaponType weaponType;
    public enum WeaponType
    {
        Slash = 0,
        Crush = 1
    }

    public int damage;
}
