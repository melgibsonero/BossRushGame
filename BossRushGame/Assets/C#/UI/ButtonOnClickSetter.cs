using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClickSetter : MonoBehaviour
{
    private Button _button;
    private Button.ButtonClickedEvent _original;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _original = _button.onClick;
    }

    public void SetOnClick(bool toNull)
    {
        if (toNull) _button.onClick = null;
        else _button.onClick = _original;
    }
}
