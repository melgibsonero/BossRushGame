using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(InputManager))]
public class BattleSystem_v2 : MonoBehaviour
{
    [SerializeField]
    private UnitSlot[] _unitSlots;
    [SerializeField]
    private BattleUnitBase[] _units;
    private bool _playerTurn = true;
    private int _playerCount, _enemyCount;
    
    public bool IsPlayerTurn { get { return _playerTurn; } }

    public Transform unitHolderParent;
    private BattleStateMachine bsMachine;

    private void Start()
    {
        bsMachine = FindObjectOfType<BattleStateMachine>();
        _unitSlots = new UnitSlot[unitHolderParent.childCount];
        _units = new BattleUnitBase[unitHolderParent.childCount];

        for (int i = 0; i < unitHolderParent.childCount; i++)
        {
            _unitSlots[i] = unitHolderParent.GetChild(i).GetComponent<UnitSlot>();
            _units[i] = _unitSlots[i].GetUnit();
            
            _units[i].isDoneForTurn = _units[i] is BattleUnitEnemy;
        }
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
            GameManager.ReloadScene();
        }

        if (_enemyCount == 0)
        {
            Debug.Log("Enemy lost");
            GameManager.ReloadScene();
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
            if (_playerTurn && unit is BattleUnitPlayer)
                (unit as BattleUnitPlayer).StartTurn();

            if (!_playerTurn && unit is BattleUnitEnemy)
                (unit as BattleUnitEnemy).StartTurn();
        }

        if (_playerTurn)
        {
            bsMachine.TransitionToState(BattleStateMachine.MenuState.ActionButtons);
        }
        else
        {
            StartCoroutine("EnemyLoop");
        }
        #endregion
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

    public BattleUnitPlayer GetPlayerUnit()
    {
        foreach (BattleUnitPlayer unit in _units)
        {
            if (unit.IsDead)
                continue;

            return unit;
        }

        Debug.LogError("NULL");
        return null;
    }

    public void PlayerDefend()
    {
        bsMachine.TransitionToState(BattleStateMachine.MenuState.Attacking);
        GetPlayerUnit().Defend();
    }

    public void AddActiveItem(GameObject item)
    {
        GameObject ItemInstance = Instantiate(item, transform.GetChild(0));

        GetPlayerUnit().AddActiveItem(ItemInstance.GetComponent<MonoBehaviour>());

        GetPlayerUnit().EndTurn();
    }
    
    IEnumerator EnemyLoop()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        while (GetUnitTurn() is BattleUnitEnemy)
        {
            (GetUnitTurn() as BattleUnitEnemy).StartAnimation();

            yield return new WaitForSecondsRealtime(1.2f);
        }
    }
    
    // button method
    public void ExitGame()
    {
        Application.Quit();
    }
}
