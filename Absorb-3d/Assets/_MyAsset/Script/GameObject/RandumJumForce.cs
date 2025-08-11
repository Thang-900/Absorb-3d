using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandumJumForce : MonoBehaviour
{
    float jumpForce = 5f; // The force applied when jumping
    float jumpInterval = 3f; // Time interval between jumps
    bool canJump = true; // Flag to check if the object can jump
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (jumpInterval >= 0 && canJump == false)
        {
            jumpInterval -= Time.deltaTime;
            jumpInterval = Random.Range(1.3f, 3f);
            canJump = true;
        }
        if (canJump)
        {
            // Apply a random jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false; // Set the flag to false to prevent immediate re-jump
        }
    }
}
