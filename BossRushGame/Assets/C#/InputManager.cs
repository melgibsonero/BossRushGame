using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private const int NEW = 0, OLD = 1;

    [SerializeField, Range(0, 1)]
    private float _axisDeadzone = 0.25f;
    
    // To add / remove axis: add / remove string and enum
    private float[,] _axes;
    private string[] _axesNames = { "Horizontal", "Vertical" };
    public enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }

    // To add / remove button: add / remove string and enum
    private bool[] _buttonOverwrites;
    private string[] _buttonNames = { "Interact" };
    public enum Button
    {
        Interact = 0
    }
    private bool _result;

    private void Start()
    {
        _axes = new float[_axesNames.Length, 2];
        _buttonOverwrites = new bool[_buttonNames.Length];
    }

    private void Update()
    {
        #region clear button overwrites

        for (int i = 0; i < _buttonOverwrites.Length; i++)
        {
            _buttonOverwrites[i] = false;
        }

        #endregion

        #region axis input

        for (int i = 0; i < _axesNames.Length; i++)
        {
            // set old input
            _axes[i, OLD] = _axes[i, NEW];

            // set new input
            _axes[i, NEW] = Input.GetAxis(_axesNames[i]);
        }

        #endregion
    }

    #region button methods

    public bool GetButtonDown(Button button)
    {
        if (_buttonOverwrites[(int)button])
            return false;

        _result = Input.GetButtonDown(_buttonNames[(int)button]);

        if (_result)
            _buttonOverwrites[(int)button] = true;

        return _result;
    }

    public bool GetButtonHold(Button button)
    {
        return Input.GetButton(_buttonNames[(int)button]);
    }

    public bool GetButtonUp(Button button)
    {
        if (_buttonOverwrites[(int)button])
            return false;

        _result = Input.GetButtonUp(_buttonNames[(int)button]);

        if (_result)
            _buttonOverwrites[(int)button] = true;

        return _result;
    }

    #endregion

    #region axis methods

    public float GetAxisValue(Axis axis)
    {
        return _axes[(int)axis, NEW];
    }

    private bool IsInDeadZone(float value)
    {
        return value >= -_axisDeadzone && value <= _axisDeadzone;
    }

    public bool GetAxisDown(Axis axis, out bool positive)
    {
        positive = _axes[(int)axis, NEW] > 0;

        return !IsInDeadZone(_axes[(int)axis, NEW]) && IsInDeadZone(_axes[(int)axis, OLD]);
    }

    public bool GetAxisHold(Axis axis)
    {
        return
            _axes[(int)axis, NEW] > _axisDeadzone && _axes[(int)axis, OLD] > _axisDeadzone ||
            _axes[(int)axis, NEW] < -_axisDeadzone && _axes[(int)axis, OLD] < -_axisDeadzone;
    }

    public bool GetAxisUp(Axis axis)
    {
        return IsInDeadZone(_axes[(int)axis, NEW]) && !IsInDeadZone(_axes[(int)axis, OLD]);
    }

    #endregion
}
