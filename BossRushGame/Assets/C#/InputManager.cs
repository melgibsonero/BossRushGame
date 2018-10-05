using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    private float _deadZone = 0.25f;

    private const int NEW = 0, OLD = 1;

    private float[,] _axes;
    private string[] _axesNames = { "Horizontal", "Vertical" };
    public enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }

    private void Start()
    {
        _axes = new float[_axesNames.Length, 2];
    }

    private void Update()
    {
        for (int i = 0; i < _axesNames.Length; i++)
        {
            // set old input
            _axes[i, OLD] = _axes[i, NEW];

            // set new input
            _axes[i, NEW] = Input.GetAxis(_axesNames[i]);
        }
    }

    private bool IsInDeadZone(float value)
    {
        return value >= -_deadZone && value <= _deadZone;
    }

    public bool GetAxisDown(Axis axis, out bool positive)
    {
        positive = _axes[(int)axis, NEW] > 0;

        return !IsInDeadZone(_axes[(int)axis, NEW]) && IsInDeadZone(_axes[(int)axis, OLD]);
    }

    public bool GetAxisHold(Axis axis)
    {
        return
            _axes[(int)axis, NEW] > _deadZone && _axes[(int)axis, OLD] > _deadZone ||
            _axes[(int)axis, NEW] < -_deadZone && _axes[(int)axis, OLD] < -_deadZone;
    }

    public bool GetAxisUp(Axis axis)
    {
        return IsInDeadZone(_axes[(int)axis, NEW]) && !IsInDeadZone(_axes[(int)axis, OLD]);
    }
}
