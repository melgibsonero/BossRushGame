using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

    public enum List
    {
        None = 0,
        Slash = 1,
        Crush = 2,
        Item = 3
    }

    private EventSystem eventSystem;
    private UnitHighlight _unitHighlight;

    public Button[] buttonList;

    public float TurnTime = 0.2f;

    public int ButtonHeight = 66;

    public bool ListOpen = false;

    public GameObject SlashList;
    public GameObject CrushList;
    public GameObject ItemList;

    public GameObject currentlySelected;
    [SerializeField]
    private GameObject oldSelected;

    // Use this for initialization
    void Start () {
        eventSystem = EventSystem.current;
        _unitHighlight = FindObjectOfType<UnitHighlight>();
        buttonList = GetComponentsInChildren<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ListOpen)
        {
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

            if (currentlySelected == null)
            {
                eventSystem.SetSelectedGameObject(oldSelected);
            }
        }
	}

    public void OpenList(int Type)
    {
        List type = (List)Type;
        if (type != List.None)
        {
            _unitHighlight.ToggleActionButtons(false);
            ListOpen = true;
            SlashList.SetActive(type == List.Slash);
            CrushList.SetActive(type == List.Crush);
            ItemList.SetActive(type == List.Item);

            Button[] abilities = GetListFromEnum(type).GetComponentsInChildren<Button>();
            OrganizeList(abilities);
            eventSystem.SetSelectedGameObject(abilities[0].gameObject);
        }
        else
        {
            _unitHighlight.ToggleActionButtons(true);
        }
    }

    private void OrganizeList(Button[] abilities)
    {
        float new_y;
        float multi = abilities.Length / 2f;
        for(int i = 0; i<abilities.Length; i++)
        {
            var tempNavigation = abilities[i].navigation;
            if (i == 0)
            {
                tempNavigation.selectOnUp = null;
                tempNavigation.selectOnDown = abilities[i + 1];
            }
            else if(i == abilities.Length - 1)
            {
                tempNavigation.selectOnUp = abilities[i - 1];
                tempNavigation.selectOnDown = null;
            }
            else
            {
                tempNavigation.selectOnUp = abilities[i - 1];
                tempNavigation.selectOnDown = abilities[i + 1];
            }

            new_y = (i - multi) * ButtonHeight + ButtonHeight / 2f;
            abilities[i].navigation = tempNavigation;
            abilities[i].transform.position = abilities[i].transform.parent.position - new Vector3(0, new_y, 0);
        }

        
    }

    private GameObject GetListFromEnum(List number)
    {
        if(number == List.Slash)
        {
            return SlashList;
        }
        if (number == List.Crush)
        {
            return CrushList;
        }
        if (number == List.Item)
        {
            return ItemList;
        }
        return null;
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
