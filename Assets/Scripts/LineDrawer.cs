using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private const float ZAxisOffset = 10f;
    private const float Resolution = 0.2f;

    [SerializeField] private GameObject _linePrefab;
 
    private Camera _camera;
    private GameObject _line;
    private LineRenderer _lineRenderer;
    private Vector3 _mousePosition;
    private Vector3 _newDrawingPoint;
    private bool _isPointerInDrawPanel;
    
    
    private void Awake()
    {
        InitializeCamera();
        InitializeLine();
    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawingNewLine();
        }
        
        if (Input.GetMouseButton(0) && _isPointerInDrawPanel)
        {
            KeepDrawingNewLine();
        }
    }

    private void InitializeCamera()
    {
        _camera = Camera.main;
    }
    
    private void InitializeLine()
    {
        _line = Instantiate(_linePrefab);
        _lineRenderer = _line.GetComponent<LineRenderer>();
    }
    
    private void StartDrawingNewLine()
    {
        _lineRenderer.positionCount = 0;
    }
    
    private void KeepDrawingNewLine()
    {
        UpdatePointerPosition();
        _newDrawingPoint = _camera.ScreenToWorldPoint(_mousePosition);
        if (CanAppendPosition(_newDrawingPoint))
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _newDrawingPoint);
        }
    }
    
    private void UpdatePointerPosition()
    {
        _mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ZAxisOffset);
    }
    
    private bool CanAppendPosition(Vector3 position)
    {
        if (_lineRenderer.positionCount == 0)
        {
            return true;
        }
        return Vector3.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position)
               > Resolution;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerInDrawPanel = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerInDrawPanel = false;
    }
}
