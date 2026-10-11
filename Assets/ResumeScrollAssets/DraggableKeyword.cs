using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DraggableKeyword : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI word;
    public ResumeScrollManager resumeScrollManager;

    Transform parentBeforeDrag;
    Vector3 originalPosition;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        originalPosition = transform.position;
        
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject[] skillInputs = GameObject.FindGameObjectsWithTag("SkillInput");

        foreach (GameObject skillInput in skillInputs) 
        {
            if (UICollisionDetector.IsOverlapping(rectTransform, skillInput.GetComponent<RectTransform>()))
            {
                Debug.Log(word.text + " overlapped with " + skillInput.name);
                skillInput.GetComponentInChildren<TextMeshProUGUI>().text = word.text;

                if (skillInput.name.Equals("ResumeInput"))
                {
                    resumeScrollManager.setSkill(0, word.text);
                }
                else if (skillInput.name.Equals("ResumeInput (1)"))
                {
                    resumeScrollManager.setSkill(1, word.text);
                }
                else
                {
                    resumeScrollManager.setSkill(2, word.text);
                }
            }
        }


        transform.parent = parentBeforeDrag;
        transform.position = originalPosition;
    }

}
