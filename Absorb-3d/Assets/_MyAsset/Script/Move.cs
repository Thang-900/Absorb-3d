
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    private float horizontal;
    private float vertical;
    // Update is called once per frame
    void Update()
    {
        horizontal= Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
    }
}
