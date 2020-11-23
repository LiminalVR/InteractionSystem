using App.StateMachine.Examples;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneState))]
public class SceneStateDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            var sceneState = (SceneState) target;
            if (sceneState.SceneRoot is MonoBehaviour sceneRoot)
            {
                EditorGUILayout.ObjectField("Scene Root Link", sceneRoot, typeof(ISceneRoot), true);
            }
        }
    }
}
