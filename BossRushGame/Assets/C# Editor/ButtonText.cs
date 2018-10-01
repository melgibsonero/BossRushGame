﻿using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonText : MonoBehaviour
{
    private Button[] _buttons;

    private void Update()
    {
        if (Application.isPlaying)
            return;
        
        _buttons = FindObjectsOfType<Button>();
        foreach (Button button in _buttons)
        {
            button.GetComponentInChildren<Text>().text = " " + button.name;
        }
    }
}