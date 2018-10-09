using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitEnemy : BattleUnitBase
{
    public void StartTurn()
    {
        isDoneForTurn = false;
    }

    public void ActTurn(CharCombatValues target)
    {
        target.TakeDamage(_combatValues.currentAP);

        EndTurn();
    }
}
