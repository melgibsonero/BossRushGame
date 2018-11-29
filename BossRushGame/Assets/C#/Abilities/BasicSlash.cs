using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlash : BaseAbility
{
    private Vector3 startPos;
    private Vector3 endPos;

    public ParticleSystem poof;
    
    [SerializeField]
    private Vector3 WalkUpOffset = new Vector3(-2, 0, 0);

    public override void Act(GameObject go)
    {
        base.Act(go);

        StartCoroutine("MoveToTarget");
        battleUnitAnimator.Play("MagicianWalk", 0);
    }

    public override void Retreat()
    {
        StartCoroutine("RetreatBack");
        battleUnitAnimator.Play("MagicianWalk", 0);
    }

    private IEnumerator MoveToTarget()
    {
        float timer = 0;
        startPos = Attacker.transform.position;
        endPos = Target.transform.position + WalkUpOffset;
        //float distance = Vector3.Distance(startPos, endPos) / 4f;
        Instantiate(poof, startPos, poof.transform.rotation);
        Attacker.transform.position = startPos;
        Attacker.gameObject.SetActive(false);
        while (timer < 1)
        {
            
            timer += Time.deltaTime;
            //Attacker.transform.position = Vector3.Lerp(startPos, endPos, timer / distance);
            yield return new WaitForEndOfFrame();

        }
        Attacker.gameObject.SetActive(true);
        Attacker.transform.position = endPos;
        Instantiate(poof, endPos, poof.transform.rotation);
        battleUnitAnimator.Play("DoubleStab", 0);
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
        battleUnitAnimator.Play("MagicianStanding", 0);
        EndTurn();
    }
}
