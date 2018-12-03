using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] rewardPrefabs;

    private BattleSystem_v2 _battleSystem;
    private UnitHighlight _unitHighlight;
    private WaveBase _currentWave;
    private int _childIndex = -1;

    public void Init(UnitHighlight unitHighlight, BattleSystem_v2 battleSystem)
    {
        _unitHighlight = unitHighlight;
        _battleSystem = battleSystem;
    }

    public void NextWave()
    {
        _childIndex++;
        if (_childIndex >= transform.childCount)
        {
            Debug.Log("Out of waves, reloading scene");
            GameManager.ReloadScene();
            return;
        }

        _currentWave = transform.GetChild(_childIndex).GetComponent<WaveBase>();
        
        if (_currentWave is WaveFight)
        {
            _unitHighlight.SetEnemyWave(GetEnemyWave());
            _battleSystem.UpdateUnits();
        }
        else
        {
            Debug.Log("Skipped non fight wave!");
            NextWave();
        }
    }

    private GameObject[] GetEnemyWave()
    {
        return new GameObject[]
        {
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy1],
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy2],
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy3],
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy4]
        };
    }
}
