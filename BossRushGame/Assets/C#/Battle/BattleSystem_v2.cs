using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem_v2 : MonoBehaviour
{
    private BuffSystem _buffSystem;
    private BattleUnitPlayer[] _playerUnits;
    private BattleUnitEnemy[] _enemyUnits;
    private int _unitIndex, _maxUnitIndex;

    private void Start()
    {
        _buffSystem = GetComponent<BuffSystem>();
        _playerUnits = FindObjectsOfType<BattleUnitPlayer>();
        _enemyUnits = FindObjectsOfType<BattleUnitEnemy>();
        _maxUnitIndex = _playerUnits.Length + _enemyUnits.Length;
        _unitIndex = _maxUnitIndex;
    }

    public void NextUnitsTurn()
    {
        _unitIndex++;
        if (_unitIndex >= _maxUnitIndex)
            _unitIndex = 0;

        if (_unitIndex < _playerUnits.Length)
            _playerUnits[_unitIndex].MyTurn();
        else
            _enemyUnits[_unitIndex - _playerUnits.Length].MyTurn();

    }
}
