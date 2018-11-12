﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    public BuffSystem.TriggerEndBuff attackType;

    public UnitHighlight.Targets InitTarget;

    protected Animator battleUnitAnimator;

    public BattleUnitBase Attacker; //Who attacks
    public BattleUnitBase Target; //Who gets attacked

    public BattleUnitBase[] Targets;

    public int ManaCost = 0;
    public int damage;

    
    public virtual void Act(GameObject go = null)
    {
        Attacker = FindObjectOfType<BattleSystem_v2>().GetUnitTurn();
        var isPlayer = Attacker.GetComponent<BattleUnitPlayer>();
        if (isPlayer != null)
        {
            if(attackType == BuffSystem.TriggerEndBuff.Slash)
            {
                damage = isPlayer.SlashWeapon.damage;
            }
            else
            {
                damage = isPlayer.CrushWeapon.damage;
            }
        }
        else
        {
            damage = Attacker.CombatValues.CurrentAP;
        }
        Target = go.GetComponent<BattleUnitBase>();
        battleUnitAnimator = Attacker.GetComponent<Animator>();
    }

    public void SetTargetList(BattleUnitBase[] targets)
    {
        Targets = targets;
    }

    public virtual void DealDamage()
    {
        Target.GetComponent<CharCombatValues>().TakeDamage(damage);
    }

    protected void EndTurn()
    {
        Attacker.EndTurn();
        Destroy(gameObject);
    }

    public virtual void Retreat() { }
}
