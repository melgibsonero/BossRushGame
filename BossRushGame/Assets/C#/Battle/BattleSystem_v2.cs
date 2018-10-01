using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffSystem))]
public class BattleSystem_v2 : MonoBehaviour
{
    private BuffSystem _buffSystem;
    private BattleUnitPlayer[] _playerUnits;
    private BattleUnitEnemy[] _enemyUnits;
    private int _unitIndex, _maxUnitIndex;
    private bool _playerTurn = true;

    private void Start()
    {
        _buffSystem = GetComponent<BuffSystem>();
        _playerUnits = FindObjectsOfType<BattleUnitPlayer>();
        _enemyUnits = FindObjectsOfType<BattleUnitEnemy>();
        _maxUnitIndex = _playerUnits.Length + _enemyUnits.Length;
        _unitIndex = _maxUnitIndex;

        UpdateTurnLogic();
    }

    public void UpdateTurnLogic()
    {
        #region turn done logic

        if (_playerTurn)
        {
            foreach (BattleUnitBase unit in _playerUnits)
            {
                if (!unit.isDoneForTurn)
                    return;
            }
        }
        else
        {
            foreach (BattleUnitBase unit in _enemyUnits)
            {
                if (!unit.isDoneForTurn)
                    return;
            }
        }

        #endregion

        // switch turn
        _playerTurn = !_playerTurn;

        #region start turn logic

        if (_playerTurn)
        {
            foreach (BattleUnitBase unit in _playerUnits)
            {
                unit.isDoneForTurn = false;
            }
        }
        else
        {
            foreach (BattleUnitBase unit in _enemyUnits)
            {
                unit.isDoneForTurn = false;
            }
        }

        #endregion
        
        // for turn based buffs
        _buffSystem.UpdateTurnCount(_playerTurn);
    }
}
