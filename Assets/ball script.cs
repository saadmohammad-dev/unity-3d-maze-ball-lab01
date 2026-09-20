using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // کی بورڈ کے لیے اسپیڈ

    [Header("Mouse Settings")]
    public float mouseSensitivity = 0.05f; // ماؤس ڈریگ کی حساسیت (Sensitivity)

    private Rigidbody rb;
    private Vector3 lastMousePosition;
    private bool isDragging = false;

    void Start()
    {
        // بال کے Rigidbody کمپونینٹ کو حاصل کرنا
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Mouse Drag Input Handling
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void FixedUpdate()
    {
        // --- A. Keyboard Arrow Keys / WASD Control ---
        float moveHorizontal = Input.GetAxis("Horizontal"); // Left / Right arrows
        float moveVertical = Input.GetAxis("Vertical");     // Up / Down arrows

        Vector3 keyboardMovement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed;
        rb.AddForce(keyboardMovement, ForceMode.Force);

        // --- B. Mouse Drag Movement ---
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;

            // X اور Y ماؤس ڈیلٹا کو World Space (X اور Z) میں کنورٹ کرنا
            Vector3 mouseForce = new Vector3(mouseDelta.x, 0.0f, mouseDelta.y) * mouseSensitivity;
            rb.AddForce(mouseForce, ForceMode.Impulse);

            lastMousePosition = currentMousePosition;
        }
    }
}