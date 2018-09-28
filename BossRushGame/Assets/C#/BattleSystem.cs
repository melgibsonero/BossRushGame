using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {

    [SerializeField]
    private bool playerTurn = true;
    public bool timeStopped = false;
    public bool AttackInSession;

    IEnumerator TimeStop()
    {
        timeStopped = true;
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
        timeStopped = false;
    }

    public void StopTime()
    {
        StartCoroutine("TimeStop");
    }

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
        AttackInSession = false;
        PlayerTurn = !PlayerTurn;
    }
}
