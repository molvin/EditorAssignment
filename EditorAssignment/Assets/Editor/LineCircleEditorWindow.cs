using UnityEditor;
using UnityEngine;

public class LineCircleEditorWindow : EditorWindow
{
    [SerializeField] private int _points;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _gameObject;

    private LineRenderer _lineRenderer;
    private const string EditorPrefsKey = "LineCircleWindowData";
    private const int MaxSegments = 50;
    private const float MaxRadius = 5.0f;
    private const float LabelWidth = 85.0f;
    
    [MenuItem("LineCircle/Window")]
    private static void Init()
    {
        LineCircleEditorWindow window = GetWindow<LineCircleEditorWindow>(true, "LineCircle");
        window.Show();
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            //Game object ref
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Game object: ", GUILayout.Width(LabelWidth));
                _gameObject = EditorGUILayout.ObjectField(_gameObject, typeof(GameObject), true) as GameObject;    
            }
            EditorGUILayout.EndHorizontal();
            //Segments
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Points: ", GUILayout.Width(LabelWidth));
                _points = (int) EditorGUILayout.Slider(_points, 5, MaxSegments);            
            }
            EditorGUILayout.EndHorizontal();
            //Radius
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Radius: ", GUILayout.Width(LabelWidth));
                _radius = EditorGUILayout.Slider(_radius, 0, MaxRadius);            
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        
        if (_gameObject == null || _lineRenderer == null && (_lineRenderer = _gameObject.GetComponentInChildren<LineRenderer>()) == null)
            return;

        Vector3[] positions = new Vector3[_points];
        float theta = Mathf.PI * 2.0f / (_points - 1);
        for (int i = 0; i < _points; i++)
        {
            positions[i] = new Vector3(Mathf.Cos(theta * i), 0.0f, Mathf.Sin(theta * i)) * _radius;
        }

        _lineRenderer.positionCount = _points;
        _lineRenderer.SetPositions(positions);
    }

    private void OnEnable()
    {
        string data = EditorPrefs.GetString(EditorPrefsKey, JsonUtility.ToJson(this, false));
        JsonUtility.FromJsonOverwrite(data, this);
    }
    private void OnDisable()
    {
        string data = JsonUtility.ToJson(this, false);
        EditorPrefs.SetString(EditorPrefsKey, data);
    }
}

