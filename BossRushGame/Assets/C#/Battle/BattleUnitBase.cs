using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitBase : MonoBehaviour
{
    protected CharCombatValues _combatValues;
    protected Animator _animator;
    public bool isDoneForTurn;

    [SerializeField]
    public GameObject Pointer;
    [SerializeField]
    private Vector3 Pointer_Offset;

    private void Start()
    {
        _combatValues = GetComponent<CharCombatValues>();
        _animator = GetComponent<Animator>();

        Pointer = Instantiate(Pointer, transform);
        Pointer.transform.localPosition = Pointer_Offset;
        Pointer.SetActive(true);
    }
}
