using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5f; // Tốc độ di chuyển
    public float changeDirectionInterval = 2f; // Thời gian thay đổi hướng

    private Vector3 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PickNewDirection();
        timer = 0f;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= changeDirectionInterval)
        {
            PickNewDirection();
            timer = 0f;
        }

        // Di chuyển theo hướng hiện tại
        Vector3 newPos = rb.position + moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void PickNewDirection()
    {
        // Lấy vector ngẫu nhiên trong mặt phẳng XZ
        Vector3 randomDir = Random.insideUnitSphere;
        randomDir.y = 0; // chỉ di chuyển trên mặt đất

        if (randomDir == Vector3.zero)
        {
            randomDir = Vector3.forward;
        }
        
        moveDirection = randomDir.normalized;
    }
}
