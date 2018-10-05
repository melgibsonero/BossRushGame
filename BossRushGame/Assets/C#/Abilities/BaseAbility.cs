using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    public int damage;

    public void Act(GameObject go)
    {
        go.GetComponent<CharCombatValues>().TakeDamage(damage);
    }
}
