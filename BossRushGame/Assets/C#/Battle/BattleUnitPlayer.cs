using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    public int maxCombo;
    private int _currentCombo;

    public bool interactWindow;
    private bool _interacted;

    private void Update()
    {
        if (interactWindow && _currentCombo < maxCombo)
        {
            if (Input.GetButtonDown("Interact"))
            {
                _currentCombo++;
                _interacted = true;
            }
        }
        else
        {
            _interacted = false;
        }
    }
    /*
    private bool Attacked = false;

    private int multibounceHardCap;
    
    private void Update() //TODO: Redo everything
    {

        if (JumpWindow)
        {
            if (Input.GetButtonDown("Interact") && multibounceHardCap>0)
            {
                _animator.SetBool("ContinueAttack", true);
                multibounceHardCap--;
            }
        }
        else
        {
            _animator.SetBool("ContinueAttack", false);
        }
    }

    public void InitiateAttack()
    {
        if (_BS.PlayerTurn)
        {
            if (!Attacked)
            multibounceHardCap = Random.Range(3, 8);
            _animator.SetBool("Attack", true);
            Attacked = true;
            IsAttacking();
        }
    }
    
    public void IsAttacking()
    {
        _BS.AttackInSession = !_BS.AttackInSession;
    }
    */
}
