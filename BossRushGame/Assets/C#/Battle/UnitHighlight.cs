using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitHighlight : MonoBehaviour
{
    public enum Targets
    {
        enemy = 0,
        teammate = 1,
        allEnemies = 2,
        all = 3,
        team = 4
    }
    
    [SerializeField]
    public bool _showHighlights = false;

    [HideInInspector]
    public bool HighlightAll = false; //Redundant?
    [HideInInspector]
    public bool HighlightEnemies = false; //Target only enemies
    [HideInInspector]
    public bool HighlightTeam = false;

    private bool ReadInputs = false;

    public int ActionButtonHideDistance = 500;


    [SerializeField, Tooltip("Parent of Unit Holders")]
    private GameObject UnitsParent;
    [SerializeField]
    private UnitSlot[] unitSlots;
    [SerializeField]
    private UnitSlot currentHighlight;

    private InputManager _inputManager;
    private BattleSystem_v2 _battleSystem;
    bool _inputRight;

    public EventSystem es;

    [SerializeField]
    private UIController _actionButtonWheel;

    [SerializeField]
    private GameObject CurrentAbility;
    public UnitSlot[] UnitSlots { get { return unitSlots; } }

    public void SetAbility(GameObject ability)
    {
        
        if(ability.GetComponent<BaseBuff>() != null)
        {
            Init(ability.GetComponent<BaseBuff>().InitTarget);
        }
        if (ability.GetComponent<BaseAbility>() != null)
        {
            Init(ability.GetComponent<BaseAbility>().InitTarget);
        }
        CurrentAbility = ability;
    }

    public void ToggleActionButtons(bool active)
    {
        StartCoroutine(HideActionButtons(active));
    }

    private IEnumerator HideActionButtons(bool active)
    {
        if (!active)
        {
            //es.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            ReadInputs = true;
        }
        else
        {
            ReadInputs = false;
        }
        Vector3 startPos = _actionButtonWheel.transform.position;
        Vector3 endPos;
        if (active)
        {
             endPos = _actionButtonWheel.transform.position + new Vector3(ActionButtonHideDistance, 0, 0);
            _actionButtonWheel.gameObject.SetActive(active);
        }
        else
        {
            endPos = _actionButtonWheel.transform.position + new Vector3(-ActionButtonHideDistance, 0, 0);
        }
        for (int i = 0; i <= 15; i++)
        {
            _actionButtonWheel.transform.position = Vector3.Lerp(startPos, endPos, i / 15f);
            yield return new WaitForEndOfFrame();
        }
        _actionButtonWheel.gameObject.SetActive(active);
        es.UpdateModules();
    }

    private void Start()
    {
        es = EventSystem.current;
        _actionButtonWheel = FindObjectOfType<UIController>();
        _inputManager = FindObjectOfType<InputManager>();
        _battleSystem = FindObjectOfType<BattleSystem_v2>();
        unitSlots = UnitsParent.GetComponentsInChildren<UnitSlot>();
    }

    public void Init(Targets targets = Targets.enemy)
    {
        ToggleActionButtons(false);

        _showHighlights = true;
        HighlightAll = HighlightEnemies = HighlightTeam = false;
        switch (targets)
        {
            case Targets.enemy:
                for(int i = 0; i<unitSlots.Length; i++)
                {
                    if (unitSlots[i].GetUnit().GetComponent<BattleUnitEnemy>() != null &&
                        !unitSlots[i].GetUnit().GetComponent<BattleUnitEnemy>().IsDead)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                
                break;
            case Targets.teammate:
                for (int i = 0; i < unitSlots.Length; i++)
                {
                    if (unitSlots[i].GetUnit().GetComponent<BattleUnitPlayer>() != null && 
                        !unitSlots[i].GetUnit().GetComponent<BattleUnitPlayer>().IsDead)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                break;
            case Targets.allEnemies:
                HighlightEnemies = true;
                for (int i = 0; i < unitSlots.Length; i++)
                {
                    if (unitSlots[i].GetUnit().GetComponent<BattleUnitEnemy>() != null)
                    {
                        currentHighlight = unitSlots[i];
                        break;
                    }
                }
                break;
            case Targets.all:
                HighlightAll = true;
                currentHighlight = null;
                break;
            case Targets.team:
                HighlightTeam = true;
                currentHighlight = null;
                break;
            default:
                Debug.Log("invalid choice");
                break;
        }
    }

    private void Update()
    {
        if (_showHighlights)
        {
            //If all need to be targetted
            if (HighlightAll || HighlightEnemies)
            {
                //Do something?
            }
            else
            {
                if (currentHighlight != null && _inputManager.GetAxisDown(InputManager.Axis.Horizontal, out _inputRight))
                {
                    if (_inputRight)
                    {
                        if (currentHighlight.rightUnitSlot != null)
                        {
                            currentHighlight = currentHighlight.rightUnitSlot;
                        }
                    }
                    else
                    {
                        if (currentHighlight.leftUnitSlot != null)
                        {
                            currentHighlight = currentHighlight.leftUnitSlot;
                        }
                    }
                }
            }
            if (ReadInputs  && _inputManager.GetButtonDown(InputManager.Button.Interact))
            {
                Debug.Log("acting");
                ActTarget();
            }
            if (ReadInputs && _inputManager.GetButtonDown(InputManager.Button.Cancel))
            {
                Reset();
            }
        }
        else
        {
            currentHighlight = null;
        }
    }

    public UnitSlot GetCurrentHighlight()
    {
        return currentHighlight;
    }

    public void ActTarget()
    {
        if (!HighlightAll && !HighlightEnemies && !HighlightTeam)
        {
            foreach (UnitSlot unit in unitSlots)
            {
                if (unit.IsHighlighted)
                {
                    CurrentAbility = Instantiate(CurrentAbility);
                    _battleSystem.GetUnitTurn().AbilityInUse = CurrentAbility;
                    if (CurrentAbility.GetComponent<BaseBuff>() != null)
                    {
                        CurrentAbility.GetComponent<BaseBuff>().Act(unit.GetUnit().gameObject);
                    }
                    if (CurrentAbility.GetComponent<BaseAbility>() != null)
                    {
                        CurrentAbility.GetComponent<BaseAbility>().Act(unit.GetUnit().gameObject);
                    }
                }
            }
        }
        else
        {
            CurrentAbility = Instantiate(CurrentAbility);
            _battleSystem.GetUnitTurn().AbilityInUse = CurrentAbility;
            BattleUnitBase[] targets = new BattleUnitBase[unitSlots.Length];
            var index = 0;
            for (int u = 0; u<unitSlots.Length; u++)
            {
                if (unitSlots[u].IsHighlighted)
                {
                    targets[index] = unitSlots[u].GetUnit();
                    index++;
                }
            }
            if (CurrentAbility.GetComponent<BaseAbility>() != null)
            {
                CurrentAbility.GetComponent<BaseAbility>().SetTargetList(targets);
                CurrentAbility.GetComponent<BaseAbility>().Act();
            }
        }
        Reset();
    }
    
    private void Reset()
    {
        _showHighlights = false;
        currentHighlight = null;
    }

    #region input shit 
    //Input related shit
    bool _lastInputAxisState;
    bool _lastInputAxisStateInvert;

    protected bool GetAxisAsKeyDown(string axisName, bool Invert = false)
    {
        if (!Invert)
        {
            var currentInputValue = Input.GetAxis(axisName) > 0.1;

            // prevent keep returning true when axis still pressed.
            if (currentInputValue && _lastInputAxisState)
            {
                return false;
            }

            _lastInputAxisState = currentInputValue;

            return currentInputValue;
        }
        else
        {
            var currentInputValue = Input.GetAxis(axisName) < -0.1;

            // prevent keep returning true when axis still pressed.
            if (currentInputValue && _lastInputAxisStateInvert)
            {
                return false;
            }

            _lastInputAxisStateInvert = currentInputValue;

            return currentInputValue;
        }
    }
#endregion

}

    