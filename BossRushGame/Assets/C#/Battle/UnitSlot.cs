using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour {

    public BattleUnitBase[] UnitChoices;
    private BattleUnitBase unit;

    public UnitSlot leftUnitSlot;
    public UnitSlot rightUnitSlot;

    public void Start()
    {
        unit = UnitChoices[Random.Range(0, UnitChoices.Length-1)];
    }

    public BattleUnitBase GetUnit()
    {
        return unit;
    }
}
