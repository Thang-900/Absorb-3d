using UnityEngine;

public class PlayerMoveRotation : MonoBehaviour
{
    [Header("Joystick")]
    public Joystick joystick;          // Kéo Joystick vào đây trong Inspector

    [Header("Move Settings")]
    public float moveSpeed = 5f;       // Tốc độ di chuyển

    private Vector3 moveDirection;
    //private void Start()
    //{
    //    transform.position = new Vector3(-186.76f, -332.75f, 4.62f);
    //}
    void Update()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        // Giữ đúng hướng nhưng chuẩn hóa vector để tốc độ chéo không nhanh hơn
        moveDirection = new Vector3(h, 0, v);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            // Di chuyển (đã normalized)
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

            // Quay theo hướng
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        }
    }

}
