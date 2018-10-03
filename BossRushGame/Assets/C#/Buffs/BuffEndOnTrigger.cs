using UnityEngine;

public class BuffEndOnTrigger : BaseBuff
{
    [SerializeField]
    private BuffSystem.TriggerEndBuff _trigger;

    public void TriggerRemove(BuffSystem.TriggerEndBuff trigger, CharCombatValues doer)
    {
        if (_buffOwner == doer && _trigger == trigger)
            RemoveBuff();
    }
}
