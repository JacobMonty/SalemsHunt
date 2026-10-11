using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DraggableKeyword : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentBeforeDrag;
    Vector3 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        originalPosition = transform.position;
        Debug.Log("Drag Begin");
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
        transform.parent = parentBeforeDrag;
        transform.position = originalPosition;
    }

}
