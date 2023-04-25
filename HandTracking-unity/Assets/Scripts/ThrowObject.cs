using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private bool isDragging = false;
    private Vector3 dragStartPosition;

    public float throwForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
        dragStartPosition = transform.position;
    }

    void OnMouseUp()
    {
        isDragging = false;
        Vector3 dragEndPosition = transform.position;
        Vector3 throwDirection = dragEndPosition - dragStartPosition;
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    void Update()
    {
        if (isDragging)
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane = (transform.position - mouseRay.origin).magnitude;
            Vector3 targetPosition = mouseRay.GetPoint(distanceToPlane);
            rb.MovePosition(targetPosition);
        }
    }
}