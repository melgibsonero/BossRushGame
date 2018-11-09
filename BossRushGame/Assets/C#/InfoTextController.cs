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

    private EventSystem _eventSystem;
	// Use this for initialization
	void Start () {
        _eventSystem = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update () {
		if(_eventSystem.currentSelectedGameObject != null)
        {
            TooltipText tooltip = _eventSystem.currentSelectedGameObject.GetComponent<TooltipText>();
            if(tooltip != null)
            {
                _background.SetActive(true);
                Header.SetText(tooltip.InfoHeaderText);
                BodyText.SetText(tooltip.InfoTextBody);
            }
            else
            {
                _background.SetActive(false);
            }
        }
	}
}
