using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineCircleRenderer : MonoBehaviour
{
    public float Radius;
    public int Segments;
    private LineRenderer _lineRenderer;

    private const float MinRadius = .1f;
    private const int MinSegments = 5;

    public void UpdateCircle()
    {
        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();

        Radius = Mathf.Max(Radius, MinRadius);
        Segments = Mathf.Max(Segments, MinSegments);
        
        Vector3[] positions = new Vector3[Segments];
        float theta = Mathf.PI * 2.0f / (Segments - 1);
        for (int i = 0; i < Segments; i++)
        {
            positions[i] = new Vector3(Mathf.Cos(theta * i), 0.0f, Mathf.Sin(theta * i)) * Radius;
        }

        _lineRenderer.positionCount = Segments;
        _lineRenderer.SetPositions(positions);
    }
}
