using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Rotator))]
public class RotatorEditor : Editor
{
    Rotator rotator;
    private void Awake()
    {
        rotator = (Rotator)target;
    }
    private void OnEnable()
    {
        rotator.GetComponent<Rigidbody2D>().hideFlags = HideFlags.NotEditable;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dragEffector"));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space(10f);

        if (GUILayout.Button("Apply Settings"))
        {
            rotator.ApplySettings();
        }
        
    }
}
