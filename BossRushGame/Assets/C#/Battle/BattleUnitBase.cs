using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitBase : MonoBehaviour
{
    protected CharCombatValues _combatValues;
    protected Animator _animator;
    public bool isDoneForTurn;

    private void Start()
    {
        _combatValues = GetComponent<CharCombatValues>();
        _animator = GetComponent<Animator>();
    }
}
