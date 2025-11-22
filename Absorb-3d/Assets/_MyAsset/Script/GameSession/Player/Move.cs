using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    public Joystick joystick;   // Gán joystick vào đây

    private Rigidbody rb;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Lấy input từ Joystick
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        moveDirection = new Vector3(h, 0, v).normalized;
    }

    private void FixedUpdate()
    {
        // Di chuyển bằng Rigidbody
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
