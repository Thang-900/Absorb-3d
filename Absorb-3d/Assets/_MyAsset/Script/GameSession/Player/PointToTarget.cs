using UnityEngine;

public class PointToTarget : MonoBehaviour
{
    private Vector3 lastPosition;
    private Vector3 moveDirection;
    public Transform target;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 delta = currentPosition- target.position;

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
