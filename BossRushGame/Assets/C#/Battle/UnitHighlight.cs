﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitHighlight : MonoBehaviour
{
    public enum Targets
    {
        enemy = 0,
        teammate = 1,
        allEnemies = 2,
        all = 3,
        team = 4
    }
    
    [SerializeField]
    public bool _showHighlights = false;

    [HideInInspector]
    public bool HighlightAll = false; //Redundant?
    [HideInInspector]
    public bool HighlightEnemies = false; //Target only enemies
    [HideInInspector]
    public bool HighlightTeam = false;

    public bool ReadInputs = false;
    
    [SerializeField]
    private UnitSlot[] unitSlots;
    [SerializeField]
    private UnitSlot currentHighlight;

    private InputManager _inputManager;
    private BattleSystem_v2 _battleSystem;
    bool _inputRight;

    public EventSystem es;

    private BattleStateMachine bsMachine;

    [SerializeField]
    private GameObject CurrentAbility;
    public UnitSlot[] UnitSlots { get { return unitSlots; } }

    public void SetAbility(GameObject ability)
    {
        if (ability.GetComponent<BaseAbility>() != null)
        {
            Init(ability.GetComponent<BaseAbility>().InitTarget);
        }
        CurrentAbility = ability;
    }
    
    private void Start()
    {
        es = EventSystem.current;
        _inputManager = FindObjectOfType<InputManager>();
        _battleSystem = FindObjectOfType<BattleSystem_v2>();
        bsMachine = FindObjectOfType<BattleStateMachine>();
    }

    public void Init(Targets targets = Targets.enemy)
    {
        bsMachine.TransitionToState(BattleStateMachine.MenuState.Targetting);

        _showHighlights = true;
        HighlightAll = HighlightEnemies = HighlightTeam = false;
        switch (targets)
        {
            case Targets.enemy:
                for(int i = 0; i<unitSlots.Length; i++)
                {
                    if (unitSlots[i].IsEnemy && unitSlots[i].GetUnit() != null &&
                        !unitSlots[i].GetUnit().GetComponent<BattleUnitEnemy>().IsDead)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                
                break;
            case Targets.teammate:
                for (int i = 0; i < unitSlots.Length; i++)
                {
                    if (!unitSlots[i].IsEnemy && 
                        !unitSlots[i].GetUnit().GetComponent<BattleUnitPlayer>().IsDead)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                break;
            case Targets.allEnemies:
                HighlightEnemies = true;
                for (int i = 0; i < unitSlots.Length; i++)
                {
                    if (unitSlots[i].IsEnemy)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                break;
            case Targets.all:
                HighlightAll = true;
                currentHighlight = null;
                break;
            case Targets.team:
                HighlightTeam = true;
                currentHighlight = null;
                break;
            default:
                Debug.Log("invalid choice");
                break;
        }
    }

    private void Update()
    {
        if (_showHighlights)
        {
            //If all need to be targetted
            if (HighlightAll || HighlightEnemies)
            {
                //Do something?
            }
            else
            {
                if (currentHighlight != null && _inputManager.GetAxisDown(InputManager.Axis.Horizontal, out _inputRight))
                {
                    if (_inputRight)
                    {
                        currentHighlight = GetRightUnitSlot(currentHighlight);
                    }
                    else
                    {
                        currentHighlight = GetLeftUnitSlot(currentHighlight);
                    }
                }
            }
        }
        else
        {
            currentHighlight = null;
        }
    }
    
    /// <summary>
    /// Searches alive target from right
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    public UnitSlot GetRightUnitSlot(UnitSlot current)
    {
        UnitSlot oldCurrent = current;
        while (current.rightUnitSlot != null)
        {
            //If right of current is not dead, return that
            if (current.rightUnitSlot.GetUnit() && !current.rightUnitSlot.GetUnit().IsDead)
            {
                //Debug.Log("Found target");
                return current.rightUnitSlot;
            }
            //If it is, move to it and check it's right.
            else
            {
                //Debug.Log("Right one is dead, trying right of right");
                current = current.rightUnitSlot;
            }
            //Hopefully repeat until you find a not dead one or reach rightmost.
        }
        //Debug.Log("Did not find a target, dont move");
        return oldCurrent;
    }
    /// <summary>
    /// Searches alive target from left
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    public UnitSlot GetLeftUnitSlot(UnitSlot current)
    {
        UnitSlot oldCurrent = current;
        while (current.leftUnitSlot != null)
        {
            //If left of current is not dead, return that
            if (current.leftUnitSlot.GetUnit() && !current.leftUnitSlot.GetUnit().IsDead)
            {
                //Debug.Log("Found target");
                return current.leftUnitSlot;
            }
            //If it is, move to it and check it's left.
            else
            {
                //Debug.Log("Left one is dead, trying left of left");
                current = current.leftUnitSlot;
            }
            //Hopefully repeat until you find a not dead one or reach leftmost.
        }
        //Debug.Log("Did not find a target, dont move");
        return oldCurrent;
    }

    public UnitSlot GetCurrentHighlight()
    {
        return currentHighlight;
    }

    public void ActTarget()
    {
        bsMachine.TransitionToState(BattleStateMachine.MenuState.Attacking);
        if (!HighlightAll && !HighlightEnemies && !HighlightTeam)
        {
            foreach (UnitSlot unit in unitSlots)
            {
                if (unit.IsHighlighted)
                {
                    CurrentAbility = Instantiate(CurrentAbility);
                    _battleSystem.GetUnitTurn().AbilityInUse = CurrentAbility;

                    if (CurrentAbility.GetComponent<BaseAbility>() != null)
                    {
                        CurrentAbility.GetComponent<BaseAbility>().Act(unit.GetUnit().gameObject);
                    }
                }
            }
        }
        else
        {
            CurrentAbility = Instantiate(CurrentAbility);
            _battleSystem.GetUnitTurn().AbilityInUse = CurrentAbility;
            BattleUnitBase[] targets = new BattleUnitBase[unitSlots.Length];
            var index = 0;
            for (int u = 0; u<unitSlots.Length; u++)
            {
                if (unitSlots[u].IsHighlighted)
                {
                    targets[index] = unitSlots[u].GetUnit();
                    index++;
                }
            }
            if (CurrentAbility.GetComponent<BaseAbility>() != null)
            {
                CurrentAbility.GetComponent<BaseAbility>().SetTargetList(targets);
                CurrentAbility.GetComponent<BaseAbility>().Act();
            }
        }

        _battleSystem.GetUnitTurn().CombatValues.UseMana(CurrentAbility.GetComponent<BaseAbility>().ManaCost);

        Reset();
    }
    
    public void Reset()
    {
        _showHighlights = false;
        currentHighlight = null;
    }
    
    public void SetEnemyWave(GameObject[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                unitSlots[i + 1].SetUnit(enemies[i].GetComponent<BattleUnitBase>());
        }
    }

    private void InitUnitSlots()
    {
        unitSlots = GetComponentsInChildren<UnitSlot>();
        foreach (UnitSlot slot in unitSlots)
            slot.Init(this);
    }

    public UnitSlot[] GetUnitSlots()
    {
        if (unitSlots.Length == 0) InitUnitSlots();

        return unitSlots;
    }
}

    