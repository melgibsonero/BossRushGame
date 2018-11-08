using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegenMP : MonoBehaviour
{
    [SerializeField]
    private int _amount, _rounds;

    public int GetRegenAmount()
    {
        if (_amount != 0)
            _rounds--;

        if (_rounds < 0)
            _amount = 0;

        return _amount;
    }
}
