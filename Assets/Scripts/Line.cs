using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _edgeCollider2D;
    
    private List<Vector2> _fingerPositions;

    private void Awake()
    {
        _fingerPositions = new List<Vector2>();
    }

    public void RefreshLine()
    {
        _lineRenderer.positionCount = 0;
    }

    public void SetPosition(Vector2 position)
    {
        if (!CanAppendPosition(position))
        {
            return;
        }
        
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        _fingerPositions.Add(position);
        _edgeCollider2D.points = _fingerPositions.ToArray();
    }

    private bool CanAppendPosition(Vector2 position)
    {
        if (_lineRenderer.positionCount == 0)
        {
            return true;
        }

        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position) >
               LineDrawer.RESOLUTION;
    }
}
