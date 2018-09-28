using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAD_AI : MonoBehaviour
{
    public CharCombatValues enemy;
    private CharCombatValues _combatValues;
    private BattleSystem _BS;
    private Animator _animator;
    private bool Attacked = false;

    bool hasTarget;
    bool isAlive = true;

    private void Start()
    {
        _combatValues = GetComponent<CharCombatValues>();
        _BS = FindObjectOfType<BattleSystem>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_BS.PlayerTurn)
        {
            if (!Attacked)
            {
                InitAttack();
            }
        }

        if (_combatValues.currentHP<=0 && !_BS.AttackInSession)
        {
            isAlive = false;
            _animator.SetBool("Dead", true);
        }
    }
    public void InitAttack()
    {
        Attacked = true;
        IsAttacking();
        _animator.SetBool("Attack", true);
    }
    public void IsAttacking()
    {
        _BS.AttackInSession = !_BS.AttackInSession;
    }

    public void DisableGO()
    {
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        if (enemy == null)
            return;

        _BS.StopTime();
        enemy.TakeDamage(_combatValues.currentAP);

        _animator.SetBool("Attack", false);
    }

    private void ChangeTurn()
    {
        _animator.SetBool("Attack", false);
        _BS.ChangeTurn();
        Attacked = false;
    }

    public void Defend()
    {

    }
}
