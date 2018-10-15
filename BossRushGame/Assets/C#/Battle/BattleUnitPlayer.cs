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
            if (_inputManager.GetButtonDown(InputManager.Button.Interact) && !interacted)
            {
                Debug.Log("success!");
                interactCounter++;
                interacted = true;
                
            }
        }
        else
        {
            //interacted = false;
        }
        _animator.SetBool("Interacted", interacted);
    }

    public void ClearInteract()
    {
        interacted = false;
    }

    public void StartTurn()
    {
        interacted = false;
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
