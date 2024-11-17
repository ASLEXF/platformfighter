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
        IItemManager itemManager = target as IItemManager;

        if (script != null)
        {
            if (GUILayout.Button("Activate"))
            {
                script.Activate();
            }
        }

        if (itemManager != null)
        {
            if (GUILayout.Button("Generate"))
            {
                itemManager.Generate();
            }
        }
    }
}
