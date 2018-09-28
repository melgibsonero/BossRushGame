using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    private BuffSystem _buffSystem;

    private CharCombatValues _buffOwner;
    private int _buffAP, _buffDP;

    protected void Init(CharCombatValues buffOwner, int buffAP, int buffDP)
    {
        _buffSystem = FindObjectOfType<BuffSystem>();

        _buffOwner = buffOwner;
        _buffAP = buffAP;
        _buffDP = buffDP;

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
