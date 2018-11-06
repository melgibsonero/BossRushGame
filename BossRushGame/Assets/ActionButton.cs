using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ActionButton : MonoBehaviour {

    public int location;

    private EventSystem es;
    private UIController uiController;
    private Button thisButton;
    [SerializeField]
    private GameObject buttonText;

    void Start () {
        es = EventSystem.current;
        uiController = GetComponentInParent<UIController>();
        thisButton = GetComponent<Button>();
        if(buttonText == null) buttonText = GetComponentInChildren<TextMeshProUGUI>(true).gameObject;
	}
	
	void Update () {
        if(es.currentSelectedGameObject == gameObject)
        {
            buttonText.SetActive(true);
        }
        else
        {
            buttonText.SetActive(false);
        }
	}
}
