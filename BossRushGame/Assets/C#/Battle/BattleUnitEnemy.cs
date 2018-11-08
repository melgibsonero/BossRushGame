using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitEnemy : BattleUnitBase
{
    private BattleUnitPlayer _player;

    protected override void Start()
    {
        base.Start();

        _player = _battleSystem.GetPlayerUnit();
    }

    public void StartTurn()
    {
        isDoneForTurn = false;
    }

    public void ActTurn()
    {
        _player.CombatValues.TakeDamage(_combatValues.currentAP);

        EndTurn();
    }

    #region Animation methods
    
    public void SetPlayerDefendWindow(bool value)
    {
        _player.defendWindow = value;
    }

    public void HitPlayer()
    {
        if (_player.interacted)
        {
            Debug.Log("Player defended");
        }
        else
        {
            _player.CombatValues.TakeDamage(_combatValues.currentAP);
        }

        _player.ClearInteract();
    }

    // EndTurn() from base

    #endregion
}
