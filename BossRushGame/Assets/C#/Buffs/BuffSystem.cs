using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public List<BaseBuff> _buffs;

    [Space(-10), Header("Add more PLS")]
    public TriggerEndBuff triggerEndBuff;
    public enum TriggerEndBuff
    {
        Attack = 0
    }
}
