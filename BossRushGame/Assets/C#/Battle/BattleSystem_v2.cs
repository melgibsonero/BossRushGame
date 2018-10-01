using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem_v2 : MonoBehaviour
{
    private BuffSystem _buffSystem;
    private BattleUnitPlayer[] _playerUnits;
    private BattleUnitEnemy[] _enemyUnits;

    public BattleUnitPlayer[] PlayerUnits { get { return _playerUnits; } }
    public BattleUnitEnemy[] EnemyUnits { get { return _enemyUnits; } }

    private void Start()
    {
        _buffSystem = GetComponent<BuffSystem>();
        _playerUnits = FindObjectsOfType<BattleUnitPlayer>();
        _enemyUnits = FindObjectsOfType<BattleUnitEnemy>();
    }
}
