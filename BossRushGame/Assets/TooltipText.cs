using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipText : MonoBehaviour {

    [SerializeField, TextArea, Tooltip("Text that explains briefly what ability does")]
    private string _InfoQuickTooltip = "";
    [SerializeField, TextArea, Tooltip("Brief tutorial how the skill is used")]
    private string _InfoTutorialText = "";


    public string InfoTutorial
    {
        get
        {
            return _InfoTutorialText;
        }

        set
        {
            _InfoTutorialText = value;
        }
    }
    public string InfoTooltip
    {
        get
        {
            return _InfoQuickTooltip;
        }

        set
        {
            _InfoQuickTooltip = value;
        }
    }
}
