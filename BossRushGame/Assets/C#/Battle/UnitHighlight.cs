using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlight : MonoBehaviour {

    [SerializeField]
    public bool _showHighlights = true;

    public bool HighlightAll = false;
    [SerializeField]
    private UnitSlot[] unitSlots;
    [SerializeField]
    private UnitSlot currentHighlight;


    private BaseBuff CurrentBuff;
    //private BaseAbility CurrentAbility;

    private void Start()
    {
        unitSlots = FindObjectsOfType<UnitSlot>();
        Init();
    }

    public void Init()
    {
        //Make it so that initial highlight is targetting a right target(player, enemy or all)
        currentHighlight = unitSlots[0];
        UpdatePosition();
    }

    private void Update()
    {
        //If highlighting is not needed
        if (_showHighlights)
        {
            //If all need to be targetted
            if (HighlightAll)
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
                        UpdatePosition();
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (currentHighlight.rightUnitSlot != null)
                    {
                        currentHighlight = currentHighlight.rightUnitSlot;
                        UpdatePosition();
                    }
                }

                if (currentHighlight != null && Input.GetButtonDown("Interact"))
                {
                    ActTarget();
                }
            }
        }
        else
        {
            currentHighlight = null;
        }
    }

    private void UpdatePosition()
    {
        currentHighlight.GetUnit().ShowHighlight();
    }

    public UnitSlot GetCurrentHighlight()
    {
        return currentHighlight;
    }

    private void ActTarget()
    {
        if(CurrentBuff != null)
        {
            CurrentBuff.Act(currentHighlight.gameObject);
        }
        //if(CurrentAbility != null) //Added already for later use
        //{
        //    CurrentAbility.Act(currentHighlight.gameObject);
        //}
    }
}
