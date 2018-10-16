using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : BaseAbility
{
    private static Vector3 startPos;
    private static Vector3 endPos;

    public float WaveSpeed = 0.15f;

    [SerializeField]
    private Vector3 WalkUpOffset = new Vector3(-3, 0, 0);

    public override void Act(GameObject go)
    {
        Attacker = FindObjectOfType<BattleSystem_v2>().GetUnitTurn();
        damage = Attacker.CombatValues.currentAP;
        battleUnitAnimator = Attacker.GetComponent<Animator>();

        startPos = Attacker.transform.position;
        endPos = FindObjectOfType<UnitHighlight>().UnitSlots[2].transform.position + WalkUpOffset;
        StartCoroutine("MoveToTarget");
    }

    public override void DealDamage()
    {
        StartCoroutine("DamageWave");
    }
    
    private IEnumerator DamageWave()
    {
        for (int t = 0; t < Targets.Length; t++)
        {
            if (Targets[t] != null)
            {
                Targets[t].GetComponent<CharCombatValues>().TakeDamage(damage+1);
                yield return new WaitForSeconds(WaveSpeed);
            }
        }
    }

    public override void Retreat()
    {
        StartCoroutine("RetreatBack");
    }

    private IEnumerator MoveToTarget()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            Attacker.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return new WaitForEndOfFrame();
        }
        battleUnitAnimator.Play("SmashPlaceholder", 0);
    }

    private IEnumerator RetreatBack()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            Attacker.transform.position = Vector3.Lerp(endPos, startPos, timer);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("finished" + startPos);
        EndTurn();
    }
}
