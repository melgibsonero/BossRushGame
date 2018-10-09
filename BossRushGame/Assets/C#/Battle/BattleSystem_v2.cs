using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffSystem))]
public class BattleSystem_v2 : MonoBehaviour
{
    private BuffSystem _buffSystem;
    private BattleUnitPlayer[] _playerUnits;
    private BattleUnitEnemy[] _enemyUnits;
    private bool _playerTurn = true, _battleOver;

    public BattleUnitPlayer[] PlayerUnits { get { return _playerUnits; } }
    public BattleUnitEnemy[] EnemyUnits { get { return _enemyUnits; } }
    public bool IsPlayerTurn { get { return _playerTurn; } }
    public bool IsBattleOver { get { return _battleOver; } }

    private void Start()
    {
        _buffSystem = GetComponent<BuffSystem>();
        _playerUnits = FindObjectsOfType<BattleUnitPlayer>();
        _enemyUnits = FindObjectsOfType<BattleUnitEnemy>();

        UpdateTurnLogic();
    }

    public void UpdateTurnLogic()
    {
        #region check win

        _battleOver = true;

        foreach (BattleUnitBase unit in _playerUnits)
        {
            if (!unit.IsDead)
                _battleOver = false;
        }

        if (_battleOver)
        {
            Debug.LogWarning("Enemy win");
            Debug.Break();
        }
        else
            _battleOver = true;

        foreach (BattleUnitBase unit in _enemyUnits)
        {
            if (!unit.IsDead)
                _battleOver = false;
        }

        if (_battleOver)
        {
            Debug.LogWarning("Player win");
            Debug.Break();
        }

        #endregion

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
            foreach (BattleUnitPlayer unit in _playerUnits)
            {
                unit.StartTurn();
            }
        }
        else
        {
            foreach (BattleUnitEnemy unit in _enemyUnits)
            {
                unit.StartTurn();
            }
        }

        #endregion
        
        // for turn based buffs
        _buffSystem.UpdateTurnCount(_playerTurn);
    }
}
