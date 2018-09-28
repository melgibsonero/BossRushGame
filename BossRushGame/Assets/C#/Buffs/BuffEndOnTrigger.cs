public class BuffEndOnTrigger : BaseBuff
{
    private BuffSystem.TriggerEndBuff _trigger;

    public void Init(CharCombatValues buffOwner, int buffAP, int buffDP, BuffSystem.TriggerEndBuff trigger)
    {
        Init(buffOwner, buffAP, buffDP);

        _trigger = trigger;
    }

    public void TriggerRemove(BuffSystem.TriggerEndBuff trigger)
    {
        if (_trigger == trigger)
            RemoveBuff();
    }
}
