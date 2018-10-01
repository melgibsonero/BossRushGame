using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    public int maxCombo;
    private int _currentCombo;

    public bool interactWindow;
    private bool _interacted;

    private void Update()
    {
        if (interactWindow && _currentCombo < maxCombo)
        {
            if (Input.GetButtonDown("Interact"))
            {
                _currentCombo++;
                _interacted = true;
            }
        }
        else
        {
            maxCombo = Random.Range(3, 6);
            _currentCombo = 0;
            _interacted = false;
        }
    }

    public void MyTurn()
    {

    }

    #region button calls

    public void Attack()
    {

    }

    public void Defend()
    {

    }

    #endregion
}
