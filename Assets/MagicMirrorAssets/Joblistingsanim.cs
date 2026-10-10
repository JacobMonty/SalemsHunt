using UnityEngine;
using System.Collections;




public class Joblistingsanim : MonoBehaviour
{


    [Header("Scaling Settings")]
    [SerializeField] private float moveSpeed = 5f;

    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);

    public float scaelSpeed = 10f;

    private Vector3 originalPosition;
    private bool isCentered = false;
    private Coroutine moveCoroutine;

    private Vector3 originalScale;
    private Vector3 currentTarget;
    private bool isEnglarged = false;

    private SpriteRenderer spriteRenderer;
    private int originalOrder;
    public int frontOrder = 100;
    private bool isFront = false;

    void Start() {

        originalScale = transform.localScale;
        currentTarget = originalScale;
        originalPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalOrder = spriteRenderer.sortingOrder;
    }

    void Update() {

        transform.localScale = Vector3.Lerp(transform.localScale, currentTarget, Time.deltaTime * scaelSpeed);
        

    }

    void OnMouseDown() {

        if (!isFront) {
            spriteRenderer.sortingOrder = frontOrder;
        }

        else {
            spriteRenderer.sortingOrder = originalOrder;
        }

        isFront = !isFront;

        isEnglarged = !isEnglarged;

        currentTarget = isEnglarged ? targetScale : originalScale;

        Vector3 targetPos = isCentered ? originalPosition : new Vector3(0f, 0f, transform.position.z);
        isCentered = !isCentered;

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToPosition(targetPos));

    }

    IEnumerator MoveToPosition(Vector3 target) {

        while (Vector3.Distance(transform.position, target) > 0.01f) {

            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;

        }

        transform.position = target;

    }

}
