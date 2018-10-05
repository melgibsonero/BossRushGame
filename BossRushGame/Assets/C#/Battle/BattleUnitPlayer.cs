using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : BattleUnitBase
{
    public int maxCombo;
    private int _currentCombo;

    public bool interactWindow;
    public bool interacted;

    private void Update()
    {
        if (interactWindow && _currentCombo < maxCombo)
        {
            if (Input.GetButtonDown("Interact"))
            {
                _currentCombo++;
                interacted = true;
            }
        }
        else
        {
            maxCombo = Random.Range(3, 6);
            _currentCombo = 0;
            interacted = false;
        }
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
