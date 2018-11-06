using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipText : MonoBehaviour {

    [SerializeField, TextArea]
    private string _infoHeaderText = "";
    [SerializeField, TextArea]
    private string _infoTextBody = "";

    public string InfoHeaderText
    {
        get
        {
            return _infoHeaderText;
        }

        set
        {
            _infoHeaderText = value;
        }
    }
    public string InfoTextBody
    {
        get
        {
            return _infoTextBody;
        }

        set
        {
            _infoTextBody = value;
        }
    }
}
