using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffSystem)), RequireComponent(typeof(InputManager))]
public class BattleSystem_v2 : MonoBehaviour
{
    private BuffSystem _buffSystem;
    [SerializeField]
    private UnitSlot[] _unitSlots;
    [SerializeField]
    private BattleUnitBase[] _units;
    private bool _playerTurn = true;
    private int _playerCount, _enemyCount;
    
    public bool IsPlayerTurn { get { return _playerTurn; } }

    public Transform unitHolderParent;

    private void Start()
    {
        _unitSlots = new UnitSlot[unitHolderParent.childCount];
        _units = new BattleUnitBase[unitHolderParent.childCount];
        _buffSystem = GetComponent<BuffSystem>();

        for (int i = 0; i < unitHolderParent.childCount; i++)
        {
            _unitSlots[i] = unitHolderParent.GetChild(i).GetComponent<UnitSlot>();
            _units[i] = _unitSlots[i].GetUnit();
            
            _units[i].isDoneForTurn = _units[i] is BattleUnitEnemy;
        }

        StartCoroutine("EnemyLoop");
    }

    public void UpdateTurnLogic()
    {
        #region check win

        _playerCount = 0;
        _enemyCount = 0;

        foreach (BattleUnitBase unit in _units)
        {
            if (unit.IsDead)
                continue;

            if (unit is BattleUnitPlayer)
                _playerCount++;

            if (unit is BattleUnitEnemy)
                _enemyCount++;
        }

        if (_playerCount == 0)
        {
            Debug.Log("Player lost");
            Debug.Break();
        }

        if (_enemyCount == 0)
        {
            Debug.Log("Enemy lost");
            Debug.Break();
        }

        #endregion

        #region turn done logic

        foreach (BattleUnitBase unit in _units)
        {
            if (unit.IsDead || unit.isDoneForTurn)
                continue;

            if (unit is BattleUnitPlayer && !_playerTurn)
            {
                Debug.Log("I thought that this would not come up :)");
                continue;
            }

            if (unit is BattleUnitEnemy && _playerTurn)
            {
                Debug.Log("I thought that this would not come up :)");
                continue;
            }

            return;
        }
        #endregion

        // switch turn
        _playerTurn = !_playerTurn;

        #region start turn logic

        foreach (BattleUnitBase unit in _units)
        {
            if (!_playerTurn)
                unit.isDoneForTurn = unit is BattleUnitPlayer;
            else
                unit.isDoneForTurn = unit is BattleUnitEnemy;
        }

        #endregion

        // for turn based buffs
        _buffSystem.UpdateTurnCount(_playerTurn);
    }

    public BattleUnitBase GetUnitTurn()
    {
        // find valid unit
        for (int i = 0; i < _unitSlots.Length; i++)
        {
            if (_unitSlots[i].GetUnit().IsDead || _unitSlots[i].GetUnit().isDoneForTurn)
                continue;

            if (_playerTurn && _unitSlots[i].GetUnit() is BattleUnitPlayer)
                return _unitSlots[i].GetUnit();
            if (!_playerTurn && _unitSlots[i].GetUnit() is BattleUnitEnemy)
                return _unitSlots[i].GetUnit();
        }

        // if no valid unit
        UpdateTurnLogic();

        // find valid unit again
        return GetUnitTurn();
    }

    private CharCombatValues GetPlayerCCV()
    {
        foreach (BattleUnitPlayer unit in _units)
        {
            if (unit.IsDead)
                continue;

            return unit.CombatValues;
        }

        Debug.LogError("NULL");
        return null;
    }
    
    IEnumerator EnemyLoop()
    {
        while (true)
        {
            if (!_playerTurn)
            {
                (GetUnitTurn() as BattleUnitEnemy).ActTurn(GetPlayerCCV());
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }
}
