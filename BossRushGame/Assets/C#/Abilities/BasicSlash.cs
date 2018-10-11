using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlash : BaseAbility {


    private Vector3 startPos;
    private Vector3 endPos;


    [SerializeField]
    private Vector3 WalkUpOffset = new Vector3(-2, 0, 0);

    public override void Act(GameObject go)
    {
        base.Act(go);

        StartCoroutine("MoveToTarget");
    }

    public override void Retreat()
    {
        StartCoroutine("RetreatBack");
    }

    private IEnumerator MoveToTarget()
    {
        float timer = 0;
        startPos = Attacker.transform.position;
        endPos = Defender.transform.position + WalkUpOffset;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            Attacker.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return new WaitForEndOfFrame();

        }
        battleUnitAnimator.Play("SlashPlaceholder", 0);
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
        EndTurn();
    }
}
