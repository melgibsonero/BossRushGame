using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    public int interactCounter;
    public bool interactWindow, interacted, isDefending;
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (interactWindow)
        {
            if (_inputManager.GetButtonDown(InputManager.Button.Interact) && !interacted)
            {
                interactCounter++;
                interacted = true;
            }
        }
        else
        {
            interacted = false;
        }
    }

    public void StartTurn()
    {
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
