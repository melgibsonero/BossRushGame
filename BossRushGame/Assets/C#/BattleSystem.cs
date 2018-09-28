using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {

    [SerializeField]
    private bool playerTurn = true;

    public bool PlayerTurn
    {
        get
        {
            return playerTurn;
        }

        private set
        {
            playerTurn = value;
        }
    }

    public void ChangeTurn()
    {
        PlayerTurn = !PlayerTurn;
    }
}
