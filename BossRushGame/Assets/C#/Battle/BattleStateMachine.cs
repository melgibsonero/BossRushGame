using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public bool TransitionDone { get { return panelCurve.done && listCurve.done && itemCurve.done; } }
    public UICurveLerp panelCurve, listCurve, itemCurve;
    private bool ReadInputs = false;
    private float inputBlockTimer = 0f;

    InputManager _inputManager;
    UIController _uiController;
    UnitHighlight _unitHighlight;
    EventSystem _eventSystem;

    [SerializeField]
    UIController.List LastListEnum;

    // Use this for initialization
    void Start () {
        _inputManager = FindObjectOfType<InputManager>();
        _uiController = FindObjectOfType<UIController>();
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        _eventSystem = EventSystem.current;

        TransitionToState(MenuState.ActionButtons);
    }
	
	// Update is called once per frame
	void Update () {
        if (inputBlockTimer < 0)
        {
            inputBlockTimer += Time.deltaTime;
        }
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
                    _uiController.OpenList((int)LastListEnum);
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
        {
            return;
        }
        switch (state)
        {
            case MenuState.EnemyTurn:
                currentState = MenuState.EnemyTurn;
                // curve
                panelCurve.hide = true;
                listCurve.hide = true;
                itemCurve.hide = false;
                // other
                break;
            case MenuState.ActionButtons:
                currentState = MenuState.ActionButtons;
                // curve
                panelCurve.hide = false;
                listCurve.hide = true;
                itemCurve.hide = false;
                // other
                _uiController.OpenList(0);
                break;
            case MenuState.AbilityList:
                currentState = MenuState.AbilityList;
                // curve
                panelCurve.hide = false;
                listCurve.hide = false;
                itemCurve.hide = false;
                // other
                break;
            case MenuState.Targetting:
                currentState = MenuState.Targetting;
                // curve
                panelCurve.hide = false;
                listCurve.hide = false;
                itemCurve.hide = true;
                itemCurve.target = _eventSystem.currentSelectedGameObject.transform;
                itemCurve.SetCurveStartPos(itemCurve.target.position);
                // other
                _uiController.lastSelectedItem = _eventSystem.currentSelectedGameObject;
                LastListEnum = _uiController.GetEnumFromList(_uiController.lastSelectedItem.transform.parent.gameObject);
                _uiController.OpenList(4);
                inputBlockTimer = -Time.deltaTime*3;
                break;
            case MenuState.Attacking:
                currentState = MenuState.Attacking;
                // curve
                panelCurve.hide = true;
                listCurve.hide = true;
                itemCurve.hide = false;
                itemCurve.target = null;
                // other
                _eventSystem.SetSelectedGameObject(gameObject);
                _uiController.OpenList(4);
                break;
        }
    }

    public void SetList(Transform list)
    {
        listCurve.target = list;
    }
}