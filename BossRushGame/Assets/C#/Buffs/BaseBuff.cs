using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    [SerializeField, Range(-3, 3)]
    private int _buffAP = 0, _buffDP = 0;

    private BuffSystem _buffSystem;
    protected CharCombatValues _buffOwner;
    public bool IsPlayerBuff { get { return _buffOwner.IsPlayer; } }

    public void Act(GameObject go)
    {
        _buffSystem = FindObjectOfType<BuffSystem>();

        _buffOwner = go.GetComponent<CharCombatValues>();

        AddBuff();
    }

    private void AddBuff()
    {
        _buffSystem.buffs.Add(this);

        _buffOwner.AttackBuff(_buffAP);
        _buffOwner.DefenceBuff(_buffDP);
    }

    public void RemoveBuff()
    {
        _buffSystem.buffs.Remove(this);

        _buffOwner.AttackBuff(-_buffAP);
        _buffOwner.DefenceBuff(-_buffDP);

        Destroy(gameObject);
    }
}
