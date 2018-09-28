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
    }
    public void InitAttack()
    {
        Attacked = true;
        _animator.SetBool("Attack", true);
    }

    public void Attack()
    {
        if (enemy == null)
            return;

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
