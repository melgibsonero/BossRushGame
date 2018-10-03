using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buffPrefabs;
    public List<BaseBuff> buffs;

    private UnitHighlight _unitHighlight;
    
    public enum TriggerEndBuff
    {
        Attack = 0
    }

    public void EndBuffTrigger(TriggerEndBuff trigger, CharCombatValues doer)
    {
        foreach (BuffEndOnTrigger buff in buffs)
        {
            buff.TriggerRemove(trigger, doer);
        }
    }

    public void UpdateTurnCount(bool playerBuffs)
    {
        foreach (BuffTurnLimit buff in buffs)
        {
            if (buff.IsPlayerBuff == playerBuffs)
                buff.UpdateTurnCount();
        }
    }
}
