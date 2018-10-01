public class BuffEndOnTrigger : BaseBuff
{
    private BuffSystem.TriggerEndBuff _trigger;

    public void Init(CharCombatValues buffOwner, int buffAP, int buffDP, bool isPlayerBuff, BuffSystem.TriggerEndBuff trigger)
    {
        Init(buffOwner, buffAP, buffDP, isPlayerBuff);

        _trigger = trigger;
    }

    public void TriggerRemove(BuffSystem.TriggerEndBuff trigger, CharCombatValues doer)
    {
        if (_buffOwner == doer && _trigger == trigger)
            RemoveBuff();
    }
}
