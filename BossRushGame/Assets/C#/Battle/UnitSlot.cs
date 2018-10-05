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

    [SerializeField]
    private bool isEnemy;

    public Vector3 SpawnOffset;


    public bool IsHighlighted
    {
        get
        {
            if (unitHighlight._showHighlights)
            {
                if (unitHighlight.HighlightAll)
                {
                    return true;
                }
                else if(unitHighlight.HighlightEnemies)
                {
                    return isEnemy;
                }
                else if (unitHighlight.HighlightTeam)
                {
                    return !isEnemy;
                }
                else
                {
                    if (unitHighlight.GetCurrentHighlight() == null) return false;
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
        unit = Instantiate(unit, transform.position + SpawnOffset, transform.rotation, transform);
        isEnemy = unit.GetComponent<BattleUnitEnemy>();
    }

    public BattleUnitBase GetUnit()
    {
        return unit;
    }
}
