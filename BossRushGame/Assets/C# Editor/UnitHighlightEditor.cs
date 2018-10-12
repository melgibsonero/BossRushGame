#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UnitHighlight))]
public class UnitHighlightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying) GUI.enabled = false;
        UnitHighlight myScript = (UnitHighlight)target;
        if (GUILayout.Button("Target enemy"))
        {
            myScript.Init(UnitHighlight.Targets.enemy);
        }
        if (GUILayout.Button("Target teammate"))
        {
            myScript.Init(UnitHighlight.Targets.teammate);
        }
        if (GUILayout.Button("Target team"))
        {
            myScript.Init(UnitHighlight.Targets.team);
        }
        if (GUILayout.Button("Target all enemies"))
        {
            myScript.Init(UnitHighlight.Targets.allEnemies);
        }
        if (GUILayout.Button("Target everything"))
        {
            myScript.Init(UnitHighlight.Targets.all);
        }        
    }
}

#endif
