using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    public ItemWeapon SlashWeapon;
    public ItemWeapon CrushWeapon;

    private List<MonoBehaviour> _activeItems = new List<MonoBehaviour>();

    public int interactCounter;
    public bool 
        failWindow, failedInteract, 
        interactWindow, interacted, 
        defendWindow, isDefending;
    private InputManager _inputManager;

    protected override void Start()
    {
        base.Start();
        _inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (interactWindow || defendWindow && isDefending)
        {
            if (_inputManager.GetButtonDown(InputManager.Button.Interact) && !interacted && !failedInteract)
            {
                interactCounter++;
                interacted = true;
                UpdateAnimation();
            }
        }
        else if (failWindow && _inputManager.GetButtonDown(InputManager.Button.Interact))
        {
            failedInteract = true;
        }
    }

    public void ClearInteract()
    {
        interacted = false;
        failedInteract = false;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _animator.SetBool("Interacted", interacted);
    }

    public void StartTurn()
    {
        ClearInteract();
        interactCounter = 0;
        isDefending = false;
        isDoneForTurn = false;
    }

    public override void EndTurn()
    {
        base.EndTurn();

        ClearInteract();

        #region HP and MP regen

        // HP
        int regenAmount = 0;
        foreach (MonoBehaviour regen in _activeItems)
        {
            if (regen is ItemRegenHP)
                regenAmount += (regen as ItemRegenHP).GetRegenAmount();
        }
        _combatValues.HealUp(regenAmount);

        // MP
        regenAmount = 0;
        foreach (MonoBehaviour regen in _activeItems)
        {
            if (regen is ItemRegenMP)
                regenAmount += (regen as ItemRegenMP).GetRegenAmount();
        }
        _combatValues.GetMana(regenAmount);

        _bsMachine.TransitionToState(BattleStateMachine.MenuState.EnemyTurn);

        #endregion
    }

    #region button calls

    public void Defend()
    {
        isDefending = true;
        //TODO:Fix me
        Debug.Log("Defending " + gameObject.name);
        EndTurn();
    }

    public void AddActiveItem(MonoBehaviour item)
    {
        #region use old items if possible

        if (item is ItemRegenHP)
        {
            foreach (MonoBehaviour regen in _activeItems)
            {
                if (regen is ItemRegenHP && (regen as ItemRegenHP).CanBeUsed)
                {
                    (regen as ItemRegenHP).amount = (item as ItemRegenHP).amount;
                    (regen as ItemRegenHP).rounds = (item as ItemRegenHP).rounds;
                    return;
                }
            }
        }

        if (item is ItemRegenMP)
        {
            foreach (MonoBehaviour regen in _activeItems)
            {
                if (regen is ItemRegenMP && (regen as ItemRegenMP).CanBeUsed)
                {
                    (regen as ItemRegenMP).amount = (item as ItemRegenMP).amount;
                    (regen as ItemRegenMP).rounds = (item as ItemRegenMP).rounds;
                    return;
                }
            }
        }

        #endregion

        _activeItems.Add(item);
    }

    #endregion
}
