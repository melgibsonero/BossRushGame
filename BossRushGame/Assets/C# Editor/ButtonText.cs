using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ButtonText : MonoBehaviour
{
    private ButtonNameToText[] _buttons;
    private Transform _current;

    private void Update()
    {
        //if (Application.isPlaying)
        //    return;
        
        _buttons = FindObjectsOfType<ButtonNameToText>();
        foreach (ButtonNameToText button in _buttons)
        {
            for (int i = 0; i < button.transform.GetChild(0).childCount; i++)
            {
                _current = button.transform.GetChild(0).GetChild(i);

                if (_current.name.Contains("Icon"))
                    _current.GetComponent<Image>().sprite = button.icon;

                if (_current.name.Contains("Name"))
                    _current.GetComponent<TextMeshProUGUI>().text = button.name;

                if (_current.name.Contains("MP cost"))
                {
                    if (button.manaCost == 0)
                        _current.GetComponent<TextMeshProUGUI>().text = "";
                    else
                        _current.GetComponent<TextMeshProUGUI>().text = button.manaCost + " MP";
                }
            }
        }
    }
}
