using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineCircleRenderer))]
public class LineCircleRendererEditor : Editor
{
    [SerializeField] private bool _undoSet;
    
    public override void OnInspectorGUI()
    {
        if (!_undoSet)
        {
            _undoSet = true;
            Undo.undoRedoPerformed = ((LineCircleRenderer) target).UpdateCircle;            
        }
        
        EditorGUI.BeginChangeCheck();
        
        DrawDefaultInspector();
        
        if (EditorGUI.EndChangeCheck())        
            ((LineCircleRenderer)target).UpdateCircle();
    }
    private void OnSceneGUI()
    {
        LineCircleRenderer circleRenderer = target as LineCircleRenderer;
        Vector3 position = circleRenderer.transform.position;
        Quaternion rotation = circleRenderer.transform.rotation;
        
        Handles.color = Color.cyan;      
        
        //Radius
        EditorGUI.BeginChangeCheck();

        float radius = Handles.RadiusHandle(rotation, position, circleRenderer.Radius);     
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Updated radius");
            circleRenderer.Radius = radius;
            circleRenderer.UpdateCircle();
        }
        //Segments
        EditorGUI.BeginChangeCheck();

        float size = HandleUtility.GetHandleSize(position);
        const float snap = 1f;
        float scale = Handles.ScaleSlider(circleRenderer.Segments, position, rotation * Vector3.left, Quaternion.identity, size, snap);
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Updated segments");
            circleRenderer.Segments = (int) scale;
            circleRenderer.UpdateCircle();
        }
    }
}
