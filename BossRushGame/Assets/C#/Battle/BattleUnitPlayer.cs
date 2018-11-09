using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    private List<MonoBehaviour> _activeItems = new List<MonoBehaviour>();

    public int interactCounter;
    public bool interactWindow, defendWindow, interacted, isDefending;
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
            Debug.Log("Window open, press now");
            if (_inputManager.GetButtonDown(InputManager.Button.Interact) && !interacted)
            {
                Debug.Log("success!");
                interactCounter++;
                interacted = true;
                UpdateAnimation();
            }
        }
    }

    public void ClearInteract()
    {
        interacted = false;
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
        foreach (ItemRegenHP regen in _activeItems)
        {
            regenAmount += regen.GetRegenAmount();
        }
        _combatValues.HealUp(regenAmount);

        // MP
        regenAmount = 0;
        foreach (ItemRegenMP regen in _activeItems)
        {
            regenAmount += regen.GetRegenAmount();
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

    #endregion
}
