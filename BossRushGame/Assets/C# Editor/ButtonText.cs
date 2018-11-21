using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class ButtonText : MonoBehaviour
{
    private ButtonOnClickSetter[] _buttons;
    private Transform _current;
    private int _manacost;

    private void Update()
    {
        //if (Application.isPlaying)
        //    return;
        
        _buttons = FindObjectsOfType<ButtonOnClickSetter>();
        foreach (ButtonOnClickSetter button in _buttons)
        {
            for (int i = 0; i < button.transform.GetChild(0).childCount; i++)
            {
                _current = button.transform.GetChild(0).GetChild(i);

                if (_current.name.Contains("Name"))
                    _current.GetComponent<TextMeshProUGUI>().text = button.name;

                if (_current.name.Contains("MP cost"))
                {
                    if (button.GetComponent<AbilityButton>() != null)
                        _manacost = button.GetComponent<AbilityButton>().ManaCost;
                    else
                        _manacost = 0;

                    if (_manacost == 0)
                        _current.GetComponent<TextMeshProUGUI>().text = "";
                    else
                        _current.GetComponent<TextMeshProUGUI>().text = _manacost + " MP";
                }
            }
        }
    }
}
