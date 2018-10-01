public class BuffTurnLimit : BaseBuff
{
    private int _turnsLeft;

    public void Init(CharCombatValues buffOwner, int buffAP, int buffDP, bool isPlayerBuff, int turnsLeft)
    {
        Init(buffOwner, buffAP, buffDP, isPlayerBuff);

        _turnsLeft = turnsLeft;
    }

    public void UpdateTurnCount()
    {
        _turnsLeft--;

        if (_turnsLeft <= 0)
            RemoveBuff();
    }
}
