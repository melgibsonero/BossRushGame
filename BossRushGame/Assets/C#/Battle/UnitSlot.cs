using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour {

    public BattleUnitBase[] UnitChoices;
    [SerializeField]
    private BattleUnitBase unit;
    private UnitHighlight unitHighlight;

    public UnitSlot leftUnitSlot;
    public UnitSlot rightUnitSlot;

    private bool isHighlighted;

    public Vector3 SpawnOffset;
    
        
    public bool IsHighlighted
    { get
        {
            if (unitHighlight._showHighlights)
            {
                if (unitHighlight.HighlightAll)
                {
                    return true;
                }
                else
                {
                    if (unitHighlight.GetCurrentHighlight() == null) unitHighlight.Init();
                    return unitHighlight.GetCurrentHighlight().GetUnit() == unit;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public void Start()
    {
        unitHighlight = FindObjectOfType<UnitHighlight>();
        unit = UnitChoices[Random.Range(0, UnitChoices.Length-1)];
        Instantiate(unit, transform.position + SpawnOffset, transform.rotation, transform);
    }

    public BattleUnitBase GetUnit()
    {
        return unit;
    }
}
