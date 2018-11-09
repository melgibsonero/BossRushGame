using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InfoTextController : MonoBehaviour {

    [SerializeField]
    private GameObject _background;

    [SerializeField]
    private TextMeshProUGUI Header;
    [SerializeField]
    private TextMeshProUGUI BodyText;

    [SerializeField]
    private GameObject currentSelect;

    private EventSystem _eventSystem;
	// Use this for initialization
	void Start () {
        _eventSystem = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update () {
		if(_eventSystem.currentSelectedGameObject != null)
        {
            currentSelect = _eventSystem.currentSelectedGameObject;
            TooltipText tooltip = currentSelect.GetComponent<TooltipText>();
            if(tooltip != null)
            {
                _background.SetActive(true);
                if(BattleStateMachine.currentState == BattleStateMachine.MenuState.Targetting || BattleStateMachine.currentState == BattleStateMachine.MenuState.Attacking)
                {
                    if (tooltip.InfoTutorial.Length > 0)
                    {
                        BodyText.SetText(tooltip.InfoTutorial);
                    }
                    else
                    {
                        _background.SetActive(false);
                    }
                    
                }
                else
                {
                    if (tooltip.InfoTooltip.Length > 0)
                    {
                        BodyText.SetText(tooltip.InfoTooltip);
                    }
                    else
                    {
                        _background.SetActive(false);
                    }
                }
            }
            else
            {
                _background.SetActive(false);
            }
        }
	}
}
