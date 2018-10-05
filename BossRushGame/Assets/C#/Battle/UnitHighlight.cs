using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlight : MonoBehaviour {

    public enum Targets
    {
        enemy = 0,
        teammate = 1,
        multi = 2,
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

    [SerializeField]
    private UnitSlot[] unitSlots;
    [SerializeField]
    private UnitSlot currentHighlight;


    private BaseBuff CurrentBuff;
    private BaseAbility CurrentAbility;


    public void SetBuff(BaseBuff buff)
    {
        CurrentBuff = buff;
        Init(Targets.teammate);
    }

    public void SetAbility(BaseAbility ability)
    {
        CurrentAbility = ability;
        Init(Targets.enemy);
    }

    private void Start()
    {
        unitSlots = FindObjectsOfType<UnitSlot>();
    }

    public void Init(Targets targets = Targets.enemy)
    {
        _showHighlights = true;
        HighlightAll = HighlightEnemies = HighlightTeam = false;
        switch (targets)
        {
            case Targets.enemy:
                Debug.Log("first enemy should be highlighted");
                for(int i = 0; i<unitSlots.Length; i++)
                {
                    if(unitSlots[i].GetUnit().GetComponent<BattleUnitEnemy>() != null)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                
                break;
            case Targets.teammate:
                Debug.Log("Player should be highlighted?");
                for (int i = 0; i < unitSlots.Length; i++)
                {
                    if (unitSlots[i].GetUnit().GetComponent<BattleUnitPlayer>() != null)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                break;
            case Targets.multi:
                Debug.Log("all enemies highlighted?");
                HighlightEnemies = true;
                currentHighlight = null;
                break;
            case Targets.all:
                HighlightAll = true;
                currentHighlight = null;
                Debug.Log("All targets highlighted");
                break;
            case Targets.team:
                HighlightTeam = true;
                currentHighlight = null;
                Debug.Log("All team targetted");
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
                if (currentHighlight != null) {
                    if (GetAxisAsKeyDown("Horizontal", Invert: true))
                    {
                        if (currentHighlight.leftUnitSlot != null)
                        {
                            currentHighlight = currentHighlight.leftUnitSlot;
                        }
                    }
                    if (GetAxisAsKeyDown("Horizontal", Invert: false))
                    {
                        if (currentHighlight.rightUnitSlot != null)
                        {
                            currentHighlight = currentHighlight.rightUnitSlot;
                        }
                    }
                }
            }
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("interacted");
                ActTarget();
            }
        }
        else
        {
            currentHighlight = null;
        }
    }

    public UnitSlot GetCurrentHighlight()
    {
        return currentHighlight;
    }

    private void ActTarget()
    {
        foreach (UnitSlot unit in unitSlots)
        {
            if (unit.IsHighlighted)
            { Debug.Log(unit.name + " acted upon");
                if (CurrentBuff != null)
                {
                    CurrentBuff.Act(unit.GetUnit().gameObject);
                }
                if (CurrentAbility != null) //Added already for later use
                {
                    CurrentAbility.Act(unit.GetUnit().gameObject);
                }
            }
        }
        Reset();
    }
    
    private void Reset()
    {
        _showHighlights = false;
        currentHighlight = null;
    }
    
    
    //Input related shit
    bool _lastInputAxisState;
    bool _lastInputAxisStateInvert;

    protected bool GetAxisAsKeyDown(string axisName, bool Invert = false)
    {
        if (!Invert)
        {
            var currentInputValue = Input.GetAxis(axisName) > 0.1;

            // prevent keep returning true when axis still pressed.
            if (currentInputValue && _lastInputAxisState)
            {
                return false;
            }

            _lastInputAxisState = currentInputValue;

            return currentInputValue;
        }
        else
        {
            var currentInputValue = Input.GetAxis(axisName) < -0.1;

            // prevent keep returning true when axis still pressed.
            if (currentInputValue && _lastInputAxisStateInvert)
            {
                return false;
            }

            _lastInputAxisStateInvert = currentInputValue;

            return currentInputValue;
        }
    }
}
