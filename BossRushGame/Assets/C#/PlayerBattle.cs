using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour {

    private CombatChar combatValues;
    private BattleSystem BS;
    private Animator _animator;

    private bool Attacked = false;
    private bool Guard = false;

    private float Timer;

    private void Start()
    {
        combatValues = GetComponent<CombatChar>();
        BS = FindObjectOfType<BattleSystem>();
        _animator = GetComponent<Animator>();
    }

    private void Update() //TODO: Redo everything
    {
        if (!BS.PlayerTurn)
        {
            if (Input.GetButtonDown("Interact") && !Guard)
            {
                Guard = true;
                Timer = 0.1f;
                combatValues.currentDP = combatValues.initDP + 1;
            }
        }
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            combatValues.currentDP = combatValues.initDP;
        }
    }

    public void InitiateAttack()
    {
        if (BS.PlayerTurn)
        {
            if(!Attacked)
            _animator.SetBool("Attack", true);
            Attacked = true;
        }
    }

    private void Attack()
    {
        if (combatValues.enemy == null)
            return;

        combatValues.enemy.TakeDamage(combatValues.currentAP);

        _animator.SetBool("Attack", false);
    }

    private void TurnSwap()
    {
        Attacked = false;
        Guard = false;
        BS.ChangeTurn();
    }

    public void Defend()
    {

    }
}
