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

    public void StartAnimation()
    {
        _animator.Play("jump");
    }

    #region Animation methods

    public void FailWindowOpen()
    {
        _player.failWindow = true;
    }

    public void FailWindowClose()
    {
        _player.failWindow = false;
    }

    public void DefendWindowOpen()
    {
        _player.defendWindow = true;
    }

    public void DefendWindowClose()
    {
        _player.defendWindow = false;
    }

    public void HitPlayer()
    {
        if (_player.interacted)
        {
            Debug.Log("Player defended");
        }
        else
        {
            _player.CombatValues.TakeDamage(_combatValues.CurrentAP);
        }

        _player.ClearInteract();
    }

    public override void EndTurn()
    {

        _animator.SetBool("Attack", false);

        base.EndTurn();
    }
    
    #endregion
}