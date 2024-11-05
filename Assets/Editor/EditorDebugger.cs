using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class EditorDebugger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ITrap script = target as ITrap;

        if (script != null)
        {
            if (GUILayout.Button("Activate"))
            {
                script.Activate();
            }
        }
    }
}
