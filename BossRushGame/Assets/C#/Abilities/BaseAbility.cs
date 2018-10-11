using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    [SerializeField]
    public UnitHighlight.Targets InitTarget;

    public BattleUnitBase Attacker; //Who attacks
    public BattleUnitBase Defender; //Who gets attacked
    public int damage;

    public void Act(GameObject go)
    {
        Attacker = FindObjectOfType<BattleSystem_v2>().GetUnitTurn();
        Defender = go.GetComponent<BattleUnitBase>();

        StartCoroutine("MoveToTarget");
    }

    public IEnumerator MoveToTarget()
    {
        float timer = 0;
        Vector3 startPos = Attacker.transform.position;
        Vector3 endPos = Defender.transform.position;
        while (timer < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        DealDamage();
        EndTurn();
    }

    public void DealDamage()
    {
        Defender.GetComponent<CharCombatValues>().TakeDamage(damage);
    }

    private void EndTurn()
    {
        FindObjectOfType<BattleSystem_v2>().GetUnitTurn().EndTurn();
    }
}
