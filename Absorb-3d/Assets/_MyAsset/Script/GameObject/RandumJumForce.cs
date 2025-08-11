using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandumJumForce : MonoBehaviour
{
    public float jumpForce = 5f; // The force applied when jumping
    public float jumpInterval = 3f; // Time interval between jumps
    public bool canJump = true; // Flag to check if the object can jump
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (jumpInterval > 0)
        {
            jumpInterval -= Time.deltaTime;
        }
        else
        {
            canJump = true;
            jumpInterval = Random.Range(2f, 8f);
        }
        if (canJump)
        {
            // Apply a random jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false; // Set the flag to false to prevent immediate re-jump
        }
        if(rb.velocity.y>0)
        {
            rb.rotation = Quaternion.Euler(-15, rb.rotation.y, rb.rotation.z);
        }
       if(rb.velocity.y<0)
        {
            rb.rotation = Quaternion.Euler(8, rb.rotation.y, rb.rotation.z);
        }
        else
        {
            rb.rotation = Quaternion.Euler(0, rb.rotation.y, rb.rotation.z);
        }
    }
}
