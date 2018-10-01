using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    private BuffSystem _buffSystem;

    protected CharCombatValues _buffOwner;
    private int _buffAP, _buffDP;
    public bool isPlayerBuff;

    protected void Init(CharCombatValues buffOwner, int buffAP, int buffDP, bool isPlayerBuff)
    {
        _buffSystem = FindObjectOfType<BuffSystem>();

        _buffOwner = buffOwner;
        _buffAP = buffAP;
        _buffDP = buffDP;

        this.isPlayerBuff = isPlayerBuff;

        AddBuff();
    }

    private void AddBuff()
    {
        _buffSystem._buffs.Add(this);

        _buffOwner.AttackBuff(_buffAP);
        _buffOwner.DefenceBuff(_buffDP);
    }

    public void RemoveBuff()
    {
        _buffSystem._buffs.Remove(this);

        _buffOwner.AttackBuff(-_buffAP);
        _buffOwner.DefenceBuff(-_buffDP);

        Destroy(gameObject);
    }
}
