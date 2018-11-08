using UnityEngine;

public class BuffTurnLimit : BaseBuff
{
    [SerializeField]
    private int _turnsLeft;

    public void UpdateTurnCount()
    {
        _turnsLeft--;

        if (_turnsLeft <= 0)
            RemoveBuff();
    }
}
