using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathManager))]
public class PathEditor:Editor
{
    private void OnSceneGUI()
    {
        PathManager pathManager = (PathManager)target;
        if (pathManager._wayPoints == null) return;

        for(int i = 0; i < pathManager._wayPoints.Length; i++)
        {
            if (pathManager._wayPoints[i] == null) continue;

            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(pathManager._wayPoints[i].position, Quaternion.identity);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(pathManager._wayPoints[i], "Move Point");
                pathManager._wayPoints[i].position = newPos;
            }
        }
    }
}
