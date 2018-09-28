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

    private float Timer;

    private void Start()
    {
        _combatValues = GetComponent<CharCombatValues>();
        _BS = FindObjectOfType<BattleSystem>();
        _animator = GetComponent<Animator>();
    }

    private void Update() //TODO: Redo everything
    {
        if (!_BS.PlayerTurn)
        {
            if (Input.GetButtonDown("Interact") && !Guard)
            {
                Guard = true;
                Timer = 0.1f;
                _combatValues.currentDP = _combatValues.defaultDP + 1;
            }
        }
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            _combatValues.currentDP = _combatValues.defaultDP;
        }
    }

    public void InitiateAttack()
    {
        if (_BS.PlayerTurn)
        {
            if(!Attacked)
            _animator.SetBool("Attack", true);
            Attacked = true;
        }
    }

    private void Attack()
    {
        if (enemy == null)
            return;

        enemy.TakeDamage(_combatValues.currentAP);

        _animator.SetBool("Attack", false);
    }

    private void TurnSwap()
    {
        Attacked = false;
        Guard = false;
        _BS.ChangeTurn();
    }

    public void Defend()
    {

    }
}
