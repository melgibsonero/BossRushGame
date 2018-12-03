using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem_v2 : MonoBehaviour
{
    [SerializeField]
    private UnitSlot[] _unitSlots;
    [SerializeField]
    private BattleUnitBase[] _units;
    private bool _playerTurn = true;
    private int _playerCount, _enemyCount;
    
    public bool IsPlayerTurn { get { return _playerTurn; } }
    
    private BattleStateMachine bsMachine;
    private WaveManager _waveManager;

    private void Start()
    {
        bsMachine = FindObjectOfType<BattleStateMachine>();
        _waveManager = FindObjectOfType<WaveManager>();
        
        UnitHighlight unitHighlight = FindObjectOfType<UnitHighlight>();
        _unitSlots = unitHighlight.GetUnitSlots();
        _units = new BattleUnitBase[_unitSlots.Length];

        _waveManager.Init(unitHighlight, this);
        _waveManager.NextWave();
    }

    public void UpdateTurnLogic()
    {
        #region check win

        _playerCount = 0;
        _enemyCount = 0;

        foreach (BattleUnitBase unit in _units)
        {
            if (!unit || unit.IsDead)
                continue;

            if (unit is BattleUnitPlayer)
                _playerCount++;

            if (unit is BattleUnitEnemy)
                _enemyCount++;
        }

        if (_playerCount == 0)
        {
            Debug.Log("Player lost, reloading scene");
            GameManager.ReloadScene();
        }

        if (_enemyCount == 0)
        {
            Debug.Log("Enemy lost, loading new wave");
            _waveManager.NextWave();
        }

        #endregion

        #region turn done logic

        foreach (BattleUnitBase unit in _units)
        {
            if (!unit || unit.IsDead || unit.isDoneForTurn)
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
        for (int i = 0; i < _units.Length; i++)
        {
            if (!_units[i] || _units[i].IsDead || _units[i].isDoneForTurn)
                continue;

            if (_playerTurn && _units[i] is BattleUnitPlayer)
                return _units[i];
            if (!_playerTurn && _units[i] is BattleUnitEnemy)
                return _units[i];
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
            if (!unit || unit.IsDead)
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
        bsMachine.TransitionToState(BattleStateMachine.MenuState.Attacking);

        GameObject ItemInstance = Instantiate(item, transform.GetChild(0));

        GetPlayerUnit().AddActiveItem(ItemInstance.GetComponent<MonoBehaviour>());

        GetPlayerUnit().EndTurn();
    }

    public void UpdateUnits()
    {
        for (int i = 0; i < _unitSlots.Length; i++)
        {
            _units[i] = _unitSlots[i].GetUnit();
        }
    }
    
    IEnumerator EnemyLoop()
    {
        yield return new WaitForSecondsRealtime(1f);

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
