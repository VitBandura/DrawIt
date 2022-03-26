using UnityEngine;

public class LineDrawer : MonoBehaviour
{
   public const float RESOLUTION = 0.2f;
    
   private Camera _camera;
   [SerializeField] private GameObject _linePrefab;
   
   private GameObject _currentLine;
   private Line _lineComponentInCurrentLine;
   
   private void Start()
   {
       _camera = Camera.main;
       InitializeLine();
   }

   private void Update()
   {
       Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
       
       if (Input.GetMouseButtonDown(0))
       {
           StartDrawingLine(mousePosition);
       }
      
       if (Input.GetMouseButton(0))
       {
           KeepDrawingLine(mousePosition);
       }
   }
   
   private void InitializeLine()
   {
       _currentLine = Instantiate(_linePrefab);
       _lineComponentInCurrentLine = _currentLine.GetComponent<Line>();
   }
   
   private void StartDrawingLine(Vector2 mousePosition)
   {
       _lineComponentInCurrentLine.RefreshLine();
       _currentLine.transform.position = mousePosition;
   }
  
   private void KeepDrawingLine(Vector2 mousePosition)
   {
       _lineComponentInCurrentLine.SetPosition(mousePosition);
   }

   
   
}
