using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BallPhysics))]
public class BallPhysicsEditor : Editor
{
    BallPhysics ballPhysics;

    private void Awake()
    {
        ballPhysics = (BallPhysics)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10f);

        if (GUILayout.Button("Apply"))
        {
            ballPhysics.ApplySettings();
        }
    }
}
