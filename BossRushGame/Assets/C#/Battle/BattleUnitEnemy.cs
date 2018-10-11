using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitEnemy : BattleUnitBase
{
    public void StartTurn()
    {
        isDoneForTurn = false;
    }

    public void ActTurn(BattleUnitPlayer target)
    {
        target.CombatValues.TakeDamage(_combatValues.currentAP);

        EndTurn();
    }
}
