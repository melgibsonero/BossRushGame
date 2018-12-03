using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitEnemy : BattleUnitBase
{
    private BattleUnitPlayer _player;

    public BaseAbility[] _abilities;

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
        StartCoroutine(MoveToPoint(_player.GetPointofAttack)); 
    }

    IEnumerator MoveToPoint(Vector3 point)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = point;
        Vector3 midpoint;

        _animator.Play("jump");

        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, point, timer);
            yield return new WaitForEndOfFrame();
        }
        timer = 0;
        midpoint = new Vector3(point.x + 4f, startPos.y, startPos.z/2f);
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.5f;
            transform.position = Vector3.Lerp(point, midpoint, timer);
            yield return new WaitForEndOfFrame();
        }
        
        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.7f;
            transform.position = Vector3.Lerp(midpoint, startPos, timer);
            yield return new WaitForEndOfFrame();
        }
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
        if (!_player.failedInteract && _player.interacted)
        {
            Debug.Log("Player defended");
            _player._animator.Play("Magician_Defend");
        }
        else
        {
            _player.CombatValues.TakeDamage(_combatValues.CurrentAP);
            if (_combatValues.CurrentAP < 5)
            {
                _player._animator.Play("Magician_TakeDamage");
            }
            else
            {
                _player._animator.Play("Magician_TakeAlotOfDamage");
            }
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