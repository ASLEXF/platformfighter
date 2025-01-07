using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ResolutionExample), true)]
public class ResolutionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ResolutionExample example = (ResolutionExample)target;

        Resolution resolution = example.resolution;

        EditorGUILayout.LabelField(example.resolution.ToString());

        resolution.width = EditorGUILayout.IntField("Width", resolution.width);
        resolution.height = EditorGUILayout.IntField("Height", resolution.height);
        resolution.refreshRate = EditorGUILayout.IntField("Refresh Rate", resolution.refreshRate);

        example.resolution = resolution;
    }
}
