using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleStateMachine : MonoBehaviour {

    public enum MenuState
    {
        EnemyTurn = 0,
        ActionButtons = 1,
        AbilityList = 2,
        Targetting = 3,
        Attacking = 4
    }

    public static MenuState currentState = MenuState.ActionButtons;

    [SerializeField]
    private MenuState currentStateforEditor;


    [SerializeField]
    private bool Transitioning = false;
    private bool ReadInputs = false;
    private float inputBlockTimer = 0f;

    public int ActionButtonHideDistance = 500;

    InputManager _inputManager;
    UIController _uiController;
    UnitHighlight _unitHighlight;
    EventSystem _eventSystem;

    [SerializeField]
    UIController.List LastListEnum;

    Vector3 WheelActivePos;
    Vector3 WheelHiddenPos;

    // Use this for initialization
    void Start () {
        _inputManager = FindObjectOfType<InputManager>();
        _uiController = FindObjectOfType<UIController>();
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        _eventSystem = EventSystem.current;

        WheelActivePos = _uiController.transform.position;
        WheelHiddenPos = _uiController.transform.position + new Vector3(-ActionButtonHideDistance, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (inputBlockTimer < 0)
        {
            inputBlockTimer += Time.deltaTime;
        }
        ReadInputs = inputBlockTimer >= 0;
        currentStateforEditor = currentState;
        if (_inputManager.GetButtonDown(InputManager.Button.Cancel) && !Transitioning)
        {
            switch (currentState)
            {
                case MenuState.EnemyTurn:
                    Debug.Log("enemy turn, cant do shit");
                    break;
                case MenuState.ActionButtons:
                    ToggleActionButtons(true);
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
                break;
            case MenuState.ActionButtons:
                currentState = MenuState.ActionButtons;
                ToggleActionButtons(true);
                _uiController.OpenList(0);
                break;
            case MenuState.AbilityList:
                currentState = MenuState.AbilityList;
                ToggleActionButtons(false);
                break;
            case MenuState.Targetting:
                currentState = MenuState.Targetting;
                _uiController.lastSelectedItem = _eventSystem.currentSelectedGameObject;
                LastListEnum = _uiController.GetEnumFromList(_uiController.lastSelectedItem.transform.parent.gameObject);
                _uiController.OpenList(4);
                inputBlockTimer = -Time.deltaTime*3;
                break;
            case MenuState.Attacking:
                currentState = MenuState.Attacking;
                _eventSystem.SetSelectedGameObject(gameObject);
                _uiController.OpenList(4);
                ToggleActionButtons(false);
                break;
        }
    }

    public void ToggleActionButtons(bool active)
    {
        if (_uiController.IsVisible != active)
        {
            StartCoroutine(HideActionButtons(active));
        }
    }

    private IEnumerator HideActionButtons(bool active)
    {
        Transitioning = true;
        Vector3 startPos;
        Vector3 endPos;  

        if (active)
        {
            _uiController.IsVisible = active;
            //
            startPos = WheelHiddenPos;
            endPos = WheelActivePos;
        }
        else
        {
            //
            startPos = WheelActivePos;
            endPos = WheelHiddenPos;
        }

        _uiController.transform.position = startPos;
        for (int i = 0; i <= 15; i++)
        {
            _uiController.transform.position = Vector3.Lerp(startPos, endPos, i / 15f);
            yield return new WaitForEndOfFrame();
        }
        _uiController.transform.position = endPos;
        _uiController.IsVisible = active;
        Transitioning = false;
    }
}