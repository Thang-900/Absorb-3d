using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    public Joystick joystick;

    private Rigidbody rb;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!joystick.isActiveAndEnabled)
        {
            moveDirection = Vector3.zero;
            joystick.ForceStop();
            return;
        }

        moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
