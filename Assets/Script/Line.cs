using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private float _startWidh;
    [SerializeField] private float _endWidh;
    [SerializeField] private int _positionCount;
    [SerializeField] private Material _material;

    private List<LineRenderer> _lines = new List<LineRenderer>();

    public void GetPath(List<Transform> path)
    {
        ClearLines();

        for (int i = 0; i < path.Count - 1; i++)
        {
            var presentSlot = path[i];
            var nextSlot = path[i + 1];
            float height = Mathf.Abs(presentSlot.transform.localScale.y - nextSlot.transform.localScale.y);
            CreateCurvedLineRenderer(presentSlot, nextSlot,height);
        }
    }

    private void ClearLines()
    {
        if (_lines.Count == 0) return;
        
            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i] != null)
                    Destroy(_lines[i].gameObject);
            }
        
        _lines.Clear();
    }

    private void CreateCurvedLineRenderer(Transform start, Transform end,float height)
    {
        LineRenderer newLine = CreateLineRender();
        Vector3 p1 = start.position + Vector3.up * height;
        Vector3 p2 = end.position + Vector3.up * height;

        if(height > 0)
        {
             p1 += Vector3.up * 1f;
             p2 += Vector3.up * 1f;
        }

        for (int i = 0; i < newLine.positionCount; i++)
        {
            float t = (float)i / (newLine.positionCount - 1);

            Vector3 p0 = start.transform.position;
            Vector3 p3 = end.transform.position;

            Vector3 pos = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
            newLine.SetPosition(i, pos);
        }

        AddLine(newLine);
    }

    private LineRenderer CreateLineRender()
    {
        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startWidth = _startWidh;
        lineRenderer.endWidth = _endWidh;
        lineRenderer.positionCount = _positionCount;
        lineRenderer.material = _material;
        return lineRenderer;
    }

    private void AddLine(LineRenderer line) => _lines.Add(line);
}
