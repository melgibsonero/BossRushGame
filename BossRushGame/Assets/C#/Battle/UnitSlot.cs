using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour
{
    [SerializeField]
    private BattleUnitBase _unit;
    private UnitHighlight _unitHighlight;

    public UnitSlot leftUnitSlot;
    public UnitSlot rightUnitSlot;

    [SerializeField]
    private bool _isEnemy;
    public bool IsEnemy { get { return _isEnemy; } }

    public Vector3 SpawnOffset;
    
    public bool IsHighlighted
    {
        get
        {
            if (_unit && !_unit.IsDead && _unitHighlight._showHighlights)
            {
                if (_unitHighlight.HighlightAll)
                {
                    return true;
                }
                else if(_unitHighlight.HighlightEnemies)
                {
                    return _isEnemy;
                }
                else if (_unitHighlight.HighlightTeam)
                {
                    return !_isEnemy;
                }
                else
                {
                    if (_unitHighlight.GetCurrentHighlight() == null) return false;
                    return _unitHighlight.GetCurrentHighlight().GetUnit() == _unit;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public void Init(UnitHighlight unitHighlight)
    {
        _unitHighlight = unitHighlight;

        if (!_isEnemy)
        {
            SetUnit(_unit);

            AbilityButton[] abilityButtons = FindObjectsOfType<AbilityButton>();
            foreach (AbilityButton abilityButton in abilityButtons)
                abilityButton.SetPlayer(_unit as BattleUnitPlayer);
        }
    }
    
    public BattleUnitBase GetUnit()
    {
        return _unit;
    }

    public void SetUnit(BattleUnitBase unit)
    {
        _unit = Instantiate(unit, transform.position + SpawnOffset, transform.rotation, transform);
        _unit.isDoneForTurn = _unit is BattleUnitEnemy;
    }
}
