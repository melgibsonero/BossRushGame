using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharCombatValues))]
public class BattleUnitBase : MonoBehaviour
{
    protected BattleSystem_v2 _battleSystem;
    protected CharCombatValues _combatValues;
    protected BattleStateMachine _bsMachine;
    [HideInInspector]
    public Animator _animator;
    public bool isDoneForTurn;

    [Tooltip("Where attacks are targetted to"), SerializeField]
    private GameObject PointofAttack;
    public Vector3 GetPointofAttack { get { return (PointofAttack != null) ? PointofAttack.transform.position : transform.position; } }

    [SerializeField]
    public GameObject Pointer;

    private static Vector3 Pointer_Offset = new Vector3(0,0.2f,0);

    [SerializeField]
    public GameObject AbilityInUse;

    public UnitSlot GetUnitSlot
    { get
        {
            var slots = FindObjectsOfType<UnitSlot>();
            foreach (var slot in slots)
            {
                if (slot.GetUnit() == this)
                {
                    return slot;
                }
            }
            return null;
        }
    }
    public CharCombatValues CombatValues { get { return _combatValues; } }
    public bool IsDead { get { return _combatValues.IsDead; } }

    private void Awake()
    {
        _combatValues = GetComponent<CharCombatValues>();
    }

    protected virtual void Start()
    {
        if (PointofAttack == null)
        {
            Debug.Log(gameObject.name + " missing point of attack");
        }

        _battleSystem = FindObjectOfType<BattleSystem_v2>();
        _bsMachine = FindObjectOfType<BattleStateMachine>();
        _animator = GetComponent<Animator>();

        Pointer = Instantiate(Pointer, transform);
        Pointer.transform.position = (PointofAttack!=null)? PointofAttack.transform.position + Pointer_Offset : transform.position + Pointer_Offset;
        Pointer.SetActive(true);
    }

    public virtual void EndTurn()
    {
        isDoneForTurn = true;

        _battleSystem.UpdateTurnLogic();
    }

    public void DealDamage()
    {
        if (AbilityInUse.GetComponent<BaseAbility>() != null)
        {
            AbilityInUse.GetComponent<BaseAbility>().DealDamage();
        }
    }

    public void Retreat()
    {
        if (AbilityInUse.GetComponent<BaseAbility>() != null)
        {
            AbilityInUse.GetComponent<BaseAbility>().Retreat();
        }
    }

    private void OnDrawGizmos()
    {
        if (PointofAttack != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PointofAttack.transform.position, 0.1f);
        }
    }
}
