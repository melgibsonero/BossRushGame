using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum MenuState
    {
        EnemyTurn = 0,
        ActionButtons = 1,
        AbilityList = 2,
        Targetting = 3,
        Attacking = 4
    }

    public static MenuState currentState = MenuState.EnemyTurn;

    [SerializeField]
    private MenuState currentStateforEditor;

    public bool TransitionDone { get { return !panelCurve.enabled && !listCurve.enabled && !itemCurve.enabled; } }
    public UICurveLerp panelCurve, listCurve, itemCurve;
    private bool ReadInputs = false, _checkOnClicks = false;
    private float inputBlockTimer = 0f;

    InputManager _inputManager;
    UIController _uiController;
    UnitHighlight _unitHighlight;
    EventSystem _eventSystem;

    [SerializeField]
    UIController.List LastListEnum;

    private ButtonOnClickSetter[] _allButtons;

    // Use this for initialization
    void Start ()
    {
        _allButtons = FindObjectsOfType<ButtonOnClickSetter>();
        _inputManager = FindObjectOfType<InputManager>();
        _uiController = FindObjectOfType<UIController>();
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        _eventSystem = EventSystem.current;

        TransitionToState(MenuState.ActionButtons);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_checkOnClicks && TransitionDone)
        {
            foreach (ButtonOnClickSetter b in _allButtons)
                b.SetOnClick(toNull: false);

            _checkOnClicks = false;
        }

        if (inputBlockTimer < 0)
            inputBlockTimer += Time.deltaTime;
        ReadInputs = inputBlockTimer >= 0;
        currentStateforEditor = currentState;

        if (_inputManager.GetButtonDown(InputManager.Button.Cancel) && TransitionDone)
        {
            switch (currentState)
            {
                case MenuState.EnemyTurn:
                    Debug.Log("enemy turn, cant do shit");
                    break;
                case MenuState.ActionButtons:
                    break;
                case MenuState.AbilityList:
                    TransitionToState(MenuState.ActionButtons);
                    break;
                case MenuState.Targetting:
                    TransitionToState(MenuState.AbilityList);
                    _uiController.OpenList(LastListEnum);
                    _eventSystem.SetSelectedGameObject(_uiController.lastSelectedItem);
                    _unitHighlight.Reset();
                    break;
                case MenuState.Attacking:
                    break;
            }
        }

        if (currentState == MenuState.Targetting && ReadInputs && _inputManager.GetButtonDown(InputManager.Button.Interact))
        {
            Debug.Log("acting");
            _unitHighlight.ActTarget();
        }
    }

    public void TransitionToState(MenuState state)
    {
        if (state == currentState)
            return;
        
        switch (state)
        {
            case MenuState.EnemyTurn:
                currentState = MenuState.EnemyTurn;
                // curve
                // other
                break;
            case MenuState.ActionButtons:
                currentState = MenuState.ActionButtons;
                // curve
                panelCurve.WakeUp(show: true);
                listCurve.WakeUp(show: false);
                // other
                _uiController.OpenList(UIController.List.None);
                break;
            case MenuState.AbilityList:
                currentState = MenuState.AbilityList;
                // curve
                listCurve.WakeUp(show: true);
                itemCurve.WakeUp(show: false, nullTarget: true);
                // other
                break;
            case MenuState.Targetting:
                currentState = MenuState.Targetting;
                // curve
                itemCurve.target = _eventSystem.currentSelectedGameObject.transform;
                itemCurve.SetCurveStartPos(itemCurve.target.position);
                itemCurve.WakeUp(show: true);
                // other
                _uiController.lastSelectedItem = _eventSystem.currentSelectedGameObject;
                LastListEnum = _uiController.GetEnumFromList(_uiController.lastSelectedItem.transform.parent.gameObject);
                _uiController.OpenList(UIController.List.JustHide);
                inputBlockTimer = -Time.deltaTime*3;
                break;
            case MenuState.Attacking:
                currentState = MenuState.Attacking;
                // curve
                panelCurve.WakeUp(show: false);
                listCurve.WakeUp(show: false);
                itemCurve.WakeUp(show: false);
                itemCurve.target = null;
                // other
                _eventSystem.SetSelectedGameObject(gameObject);
                _uiController.OpenList(UIController.List.JustHide);
                break;
        }

        foreach (ButtonOnClickSetter b in _allButtons)
            b.SetOnClick(toNull: true);

        _checkOnClicks = true;
    }

    public void SetList(Transform list)
    {
        listCurve.target = list;

        _uiController.OpenList(_uiController.GetEnumFromList(list.gameObject));
    }
}