using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnRandomizer))]
public class SpawnRandomizerEditor : Editor
{
    SpawnRandomizer universalBall;

    private void Awake()
    {
        universalBall = (SpawnRandomizer)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sphereRadius"));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space(10f);
        if (GUILayout.Button("Instantiate"))
        {
            universalBall.SpawnRandomBalls();
        }
    }
}
