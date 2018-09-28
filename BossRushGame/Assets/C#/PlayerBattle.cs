using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public CharCombatValues enemy;
    private CharCombatValues _combatValues;
    private BattleSystem _BS;
    private Animator _animator;

    private bool Attacked = false;
    private bool Guard = false;

    private int multibounceHardCap;

    public bool JumpWindow;

    private void Start()
    {
        _combatValues = GetComponent<CharCombatValues>();
        _BS = FindObjectOfType<BattleSystem>();
        _animator = GetComponent<Animator>();
    }

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

    private void Attack()
    {
        if (enemy == null)
            return;

        _BS.StopTime();
        enemy.TakeDamage(_combatValues.currentAP);

        _animator.SetBool("Attack", false);
    }

    private void TurnSwap()
    {
        _animator.SetBool("Attack", false);
        Attacked = false;
        Guard = false;
        _BS.ChangeTurn();
    }

    public void Defend()
    {

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, transform.localScale * 2);
    }
}
