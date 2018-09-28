using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAD_AI : MonoBehaviour
{
    private CombatChar combatValues;
    private BattleSystem BS;
    private Animator _animator;
    private bool Attacked = false;

    private void Start()
    {
        combatValues = GetComponent<CombatChar>();
        BS = FindObjectOfType<BattleSystem>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!BS.PlayerTurn)
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
        if (combatValues.enemy == null)
            return;

        combatValues.enemy.TakeDamage(combatValues.currentAP);

        _animator.SetBool("Attack", false);
    }

    private void ChangeTurn()
    {
        BS.ChangeTurn();
        Attacked = false;
    }

    public void Defend()
    {

    }
}
