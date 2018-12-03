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

    public Vector3 SpawnOffset;
    
    public bool IsHighlighted
    {
        get
        {
            if (!GetUnit().IsDead && _unitHighlight._showHighlights)
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

    public void Start()
    {
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        _isEnemy = _unit.GetComponent<BattleUnitEnemy>();
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

    public void SetUnit(BattleUnitBase unit)
    {
        _unit = Instantiate(unit, transform.position + SpawnOffset, transform.rotation, transform);
    }
}

        return _unit;