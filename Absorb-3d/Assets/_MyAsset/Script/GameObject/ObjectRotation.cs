using UnityEngine;

public class ObjectMoveRotation : MonoBehaviour
{
    public Rigidbody rb;
    public float rotationSpeed;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = rb.transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = rb.transform.position;
        Vector3 delta = currentPosition - lastPosition;
        delta.y = 0; // Chỉ xét chuyển động trên mặt đất
        if (delta.magnitude > 0.01f)
        {
            Vector3 direction = (delta).normalized;
            direction.y = 0; // Giữ AI không ngẩng/nghiêng lên xuống

            // Tính toán góc quay đích
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Xoay dần sang hướng mới
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        lastPosition = currentPosition;
    }
}
