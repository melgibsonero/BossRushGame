using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    public UnitHighlight.Targets InitTarget;

    protected Animator battleUnitAnimator;

    public BattleUnitBase Attacker; //Who attacks
    public BattleUnitBase Defender; //Who gets attacked

    private int damage;

    public virtual void Act(GameObject go)
    {
        Attacker = FindObjectOfType<BattleSystem_v2>().GetUnitTurn();
        damage = Attacker.CombatValues.currentAP;
        Defender = go.GetComponent<BattleUnitBase>();
        battleUnitAnimator = Attacker.GetComponent<Animator>();
    }

    public void DealDamage()
    {
        Defender.GetComponent<CharCombatValues>().TakeDamage(damage);
    }

    protected void EndTurn()
    {
        Attacker.EndTurn();
        Destroy(gameObject);
    }

    public virtual void Retreat() { }
}
