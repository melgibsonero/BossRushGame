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
    public bool _showHighlights = true;

    public bool HighlightAll = false; //Redundant?
    public bool HighlightEnemies = false; //Target only enemies
    public bool HighlightTeam = false;

    [SerializeField]
    private UnitSlot[] unitSlots;
    [SerializeField]
    private UnitSlot currentHighlight;


    private BaseBuff CurrentBuff;
    //private BaseAbility CurrentAbility;

    private void Start()
    {
        unitSlots = FindObjectsOfType<UnitSlot>();
        
        Init(Targets.enemy);
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
        //If highlighting is not needed
        if (_showHighlights)
        {
            //If all need to be targetted
            if (HighlightAll || HighlightEnemies)
            {
                //Do something?
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (currentHighlight.leftUnitSlot != null)
                    {
                        currentHighlight = currentHighlight.leftUnitSlot;
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (currentHighlight.rightUnitSlot != null)
                    {
                        currentHighlight = currentHighlight.rightUnitSlot;
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
                    CurrentBuff.Act(unit.gameObject);
                }
                //if(CurrentAbility != null) //Added already for later use
                //{
                //    CurrentAbility.Act(unit.gameObject);
                //}
            }
        }
    }
}
