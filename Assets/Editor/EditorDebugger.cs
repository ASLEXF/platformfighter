using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class EditorDebugger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ITrap trap = target as ITrap;
        IItemManager itemManager = target as IItemManager;
        IGraphicSettings graphicSettings = target as IGraphicSettings;

        while (true)
        {
            if (trap != null)
            {
                if (GUILayout.Button("Activate"))
                {
                    trap.Activate();
                    break;
                }
            }

            if (itemManager != null)
            {
                if (GUILayout.Button("Generate"))
                {
                    itemManager.Generate();
                    break;
                }
            }

            if (graphicSettings != null)
            {
                
            }

            break;
        }
    }
}
