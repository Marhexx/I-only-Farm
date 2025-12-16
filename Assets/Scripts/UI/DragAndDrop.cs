using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector3 _dragOffset;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragOffset = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _dragOffset;
    }
}
