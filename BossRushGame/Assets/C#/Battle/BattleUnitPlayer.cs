using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    public int interactCounter;
    public bool interactWindow, interacted, isDefending;
    private InputManager _inputManager;

    protected override void Start()
    {
        base.Start();
        _inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (interactWindow)
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
    
    #region button calls

    public void Defend()
    {
        isDefending = true;
    }

    #endregion
}
