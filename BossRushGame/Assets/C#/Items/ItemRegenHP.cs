using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegenHP : MonoBehaviour
{
    public int amount, rounds;

    public int GetRegenAmount()
    {
        if (amount != 0)
            rounds--;

        if (rounds < 0)
            amount = 0;

        return amount;
    }
}
