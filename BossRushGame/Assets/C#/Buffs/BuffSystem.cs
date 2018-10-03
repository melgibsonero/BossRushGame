using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _buffTurnLimit, _buffEndOnTrigger;
    public List<BaseBuff> _buffs;
    
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

    public void UpdateTurnCount(bool playerBuffs)
    {
        foreach (BuffTurnLimit buff in _buffs)
        {
            if (buff.isPlayerBuff == playerBuffs)
                buff.UpdateTurnCount();
        }
    }
}
