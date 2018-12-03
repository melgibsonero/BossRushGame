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

    [SerializeField]
    private bool isEnemy;

    public Vector3 SpawnOffset;


    public bool IsHighlighted
    {
        get
        {
            if (!GetUnit().IsDead && unitHighlight._showHighlights)
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
        GetUnit();
        isEnemy = unit.GetComponent<BattleUnitEnemy>();
    }


    public BattleUnitBase GetUnit()
    {
        if (unit == null)
        {
            int randomEnemy = Random.Range(0, UnitChoices.Length);
            Debug.Log(transform.name+" picked enemy: "+randomEnemy);
            unit = UnitChoices[randomEnemy];
            unit = Instantiate(unit, transform.position + SpawnOffset, transform.rotation, transform);
        }
        return unit;
    }
}
