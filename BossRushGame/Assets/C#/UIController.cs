using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

    private EventSystem eventSystem;

    public Button[] buttonList;

    public float TurnTime = 0.2f;

    public GameObject currentlySelected;
    [SerializeField]
    private GameObject oldSelected;

    // Use this for initialization
    void Start () {
        eventSystem = EventSystem.current;
        buttonList = GetComponentsInChildren<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (eventSystem && oldSelected != null && oldSelected != currentlySelected && currentlySelected != null)
        {
            int dir1 = oldSelected.GetComponent<ActionButton>().location;
            int dir2 = currentlySelected.GetComponent<ActionButton>().location;
            int direction = dir2 - dir1;
            if (System.Math.Abs(direction) > 1) direction = -System.Math.Sign(direction);
            StartCoroutine(RotateIcons(direction));
        }
        oldSelected = currentlySelected;
        currentlySelected = eventSystem.currentSelectedGameObject;

        if(currentlySelected == null)
        {
            eventSystem.SetSelectedGameObject(oldSelected);
        }
	}

    IEnumerator RotateIcons(int direction)
    {
        for (int i = 0; i < 15; i++)
        {
            transform.Rotate(new Vector3(0, 0, direction*6));
            foreach (var button in buttonList)
            {
                button.transform.Rotate(new Vector3(0, 0, -direction*6));
            }
            yield return new WaitForEndOfFrame();
        }

    }
}
