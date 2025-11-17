using UnityEngine;

public class PlayerMoveRotation : MonoBehaviour
{
    private Vector3 lastPosition;
    private Vector3 moveDirection;

    private void OnEnable()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 delta = currentPosition - lastPosition;
        delta.y = 0; // Chỉ xét chuyển động trên mặt đất
        if (delta.magnitude > 0.01f)
        {
            moveDirection = delta.normalized;
            // Quay object theo hướng di chuyển
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        lastPosition = currentPosition;
    }
}
