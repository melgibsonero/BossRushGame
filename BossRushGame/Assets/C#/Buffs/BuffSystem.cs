using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public List<BaseBuff> _buffs;

    [Space(-10), Header("Add more PLS")]
    public TriggerEndBuff triggerEndBuff;
    public enum TriggerEndBuff
    {
        Attack = 0
    }

    public void EndBuffTrigger(TriggerEndBuff trigger, CharCombatValues doer)
    {
        foreach (BuffEndOnTrigger buff in _buffs)
        {
            buff.TriggerRemove(trigger, doer);
        }
    }

    public void UpdateTurnCount()
    {
        foreach (BuffTurnLimit buff in _buffs)
        {
            buff.UpdateTurnCount();
        }
    }
}
