using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ultimate), true)]
public class UltimateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Ultimate myTarget = (Ultimate)target;

        if (GUILayout.Button("Activate"))
        {
            myTarget.Activate();
        }
    }
}
