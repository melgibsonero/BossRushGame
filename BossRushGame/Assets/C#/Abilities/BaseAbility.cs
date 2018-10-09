using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    [SerializeField]
    public UnitHighlight.Targets InitTarget;

    public BattleUnitBase Attacker;
    public BattleUnitBase Defender;
    public int damage;

    public void Act(GameObject go)
    {
        go.GetComponent<CharCombatValues>().TakeDamage(damage);
    }
}
