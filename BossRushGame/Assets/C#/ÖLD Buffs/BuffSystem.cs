using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public List<BaseBuff> buffs;

    private UnitHighlight _unitHighlight;
    
    public enum TriggerEndBuff
    {
        Slash = 0,
        Crush = 1
    }

    public void EndBuffTrigger(TriggerEndBuff trigger, CharCombatValues doer)
    {
        // from N to 0 so we can remove buffs from list during for loop
        for (int i = buffs.Count -1; i >= 0; i--)
        {
            // check type
            if (buffs[i] is BuffEndOnTrigger)
                (buffs[i] as BuffEndOnTrigger).TriggerRemove(trigger, doer);
        }
    }

    public void UpdateTurnCount(bool playerBuffs)
    {
        // from N to 0 so we can remove buffs from list during for loop
        for (int i = buffs.Count -1; i >= 0; i--)
        {
            // enemy buffs don't update on player turn and vice versa
            if (buffs[i].IsPlayerBuff != playerBuffs)
                continue;

            // check type
            if (buffs[i] is BuffTurnLimit)
                (buffs[i] as BuffTurnLimit).UpdateTurnCount();
        }
    }

    private void Start()
    {
        Debug.Log(name);
    }
}
