using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : BaseAbility {

    private Vector3 _playerStartPos;
    private Vector3 _playerEndPos;

    private BattleUnitPlayer PlayerCharacter;

    public bool interactWindow, interacted;

   [SerializeField]
    private GameObject BallOfDeath;

    private Animator ballAnimator;

    [SerializeField]
    private Vector3 WalkUpOffset = new Vector3(3, 0, 0);
    [SerializeField]
    private Vector3 BallSpawnPosition = new Vector3(0, 1, 0);

    [SerializeField]
    private int currentTarget = 0;

    public override void Act(GameObject go)
    {
        ballAnimator = GetComponent<Animator>();

        Attacker = FindObjectOfType<BattleSystem_v2>().GetUnitTurn();
        damage = Attacker.CombatValues.CurrentAP;
        battleUnitAnimator = Attacker.GetComponent<Animator>();

        _playerStartPos = Attacker.transform.position;
        _playerEndPos = Attacker.transform.position + WalkUpOffset;

        StartCoroutine("MoveToTarget");
    }

    public void SetPlayerInteractWindow(bool boolean)
    {
        Attacker.GetComponent<BattleUnitPlayer>().interactWindow = boolean;
    }

    private void Update()
    {
        interacted = Attacker.GetComponent<BattleUnitPlayer>().interacted;
        ballAnimator.SetBool("Interacted", interacted);
    }

    public void ClearInteract()
    {
        Attacker.GetComponent<BattleUnitPlayer>().interacted = false;
    }

    public override void DealDamage()
    {
        Targets[currentTarget].GetComponent<CharCombatValues>().TakeDamage(damage, attackType);
        currentTarget++;
    }

    private IEnumerator MoveToTarget()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 2;
            Attacker.transform.position = Vector3.Lerp(_playerStartPos, _playerEndPos, timer);
            yield return new WaitForEndOfFrame();

        }
        transform.position = Attacker.transform.position + BallSpawnPosition;
        Debug.Log("Ball of death active");
        BallOfDeath.SetActive(true);
        ballAnimator.Play("BouncingBall");
    }

    public override void Retreat()
    {
        Destroy(BallOfDeath);
        StartCoroutine("RetreatBack");
    }

    private IEnumerator RetreatBack()
    {
        Debug.Log("retreat started");
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            Attacker.transform.position = Vector3.Lerp(_playerEndPos, _playerStartPos, timer);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("retreat finished");
        EndTurn();
    }

    private IEnumerator MoveBall()
    {
        if (Targets[currentTarget + 1] == null)
        {
            ClearInteract();
            SetPlayerInteractWindow(false);
        }
        float timer = 0;
        Vector3 BallStartPos = transform.position;
        Vector3 BallEndPos = Targets[currentTarget].GetPointofAttack;
        while(timer < 1)
        {
            if (timer > 0.6f && Targets[currentTarget + 1] != null)
            {
                SetPlayerInteractWindow(true);
            }
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(BallStartPos, BallEndPos, timer);
            yield return new WaitForEndOfFrame();
        }

    }
}
