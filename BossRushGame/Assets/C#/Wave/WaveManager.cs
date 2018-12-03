using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] rewardPrefabs;

    private UnitHighlight _unitHighlight;
    private WaveBase _currentWave;
    private int _childIndex = -1;

    private void Start()
    {
        _unitHighlight = FindObjectOfType<UnitHighlight>();
    }

    public void NextWave()
    {
        _childIndex++;
        if (_childIndex > transform.childCount)
        {
            Debug.Log("Out of waves, reloading scene");
            GameManager.ReloadScene();
        }

        _currentWave = transform.GetChild(_childIndex).GetComponent<WaveBase>();


        if (_currentWave is WaveFight)
            _unitHighlight.SetEnemyWave(GetEnemyWave());
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
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy1 - 1],
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy2 - 1],
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy3 - 1],
            enemyPrefabs[(int)(_currentWave as WaveFight).enemy4 - 1]
        };
    }
}
