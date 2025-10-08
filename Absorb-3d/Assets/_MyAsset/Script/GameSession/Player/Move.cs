
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    private float horizontal;
    private float vertical;
    private Rigidbody rb;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontal= Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
