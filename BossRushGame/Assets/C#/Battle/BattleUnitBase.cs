using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombatValues))]
public class BattleUnitBase : MonoBehaviour
{
    protected BattleSystem_v2 _battleSystem;
    protected CharCombatValues _combatValues;
    protected Animator _animator;
    public bool isDoneForTurn;

    [SerializeField]
    public GameObject Pointer;
    [SerializeField]
    private Vector3 Pointer_Offset;

    public bool IsDead { get { return _combatValues.IsDead; } }

    protected virtual void Start()
    {
        _battleSystem = FindObjectOfType<BattleSystem_v2>();
        _combatValues = GetComponent<CharCombatValues>();
        _animator = GetComponent<Animator>();

        Pointer = Instantiate(Pointer, transform);
        Pointer.transform.localPosition = Pointer_Offset;
        Pointer.SetActive(true);
    }

    public void EndTurn()
    {
        isDoneForTurn = true;

        _battleSystem.UpdateTurnLogic();
    }
}
