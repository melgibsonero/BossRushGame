using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    public UnitHighlight.Targets InitTarget;
    [SerializeField]
    private Vector3 WalkUpOffset = new Vector3(-2, 0, 0);

    public BattleUnitBase Attacker; //Who attacks
    public BattleUnitBase Defender; //Who gets attacked
    public int damage;

    public void Act(GameObject go)
    {
        Attacker = FindObjectOfType<BattleSystem_v2>().GetUnitTurn();
        damage = Attacker.CombatValues.currentAP;
        Defender = go.GetComponent<BattleUnitBase>();

        StartCoroutine("MoveToTarget");
    }

    public IEnumerator MoveToTarget()
    {
        float timer = 0;
        Vector3 startPos = Attacker.transform.position;
        Vector3 endPos = Defender.transform.position + WalkUpOffset;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            Attacker.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return new WaitForEndOfFrame();
            
        }
        DealDamage();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Attacker.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return new WaitForEndOfFrame();
            
        }
        EndTurn();
    }

    public void DealDamage()
    {
        Defender.GetComponent<CharCombatValues>().TakeDamage(damage);
    }

    private void EndTurn()
    {
        Attacker.EndTurn();
        Destroy(gameObject);
    }
}
