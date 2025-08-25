using UnityEngine;

public class play : MonoBehaviour
{
    public float speed = 5f;        // tốc độ di chuyển
    public float mouseSensitivity = 100f; // độ nhạy chuột

    private CharacterController controller;
    private float rotationY = 0f;   // góc xoay theo trục Y

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Ẩn con trỏ chuột khi chơi
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Di chuyển WASD ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // --- Xoay theo chuột ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
