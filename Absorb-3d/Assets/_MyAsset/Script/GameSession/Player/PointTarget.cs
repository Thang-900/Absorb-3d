using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointTarget : MonoBehaviour
{
    private Transform ground;
    private Vector3 moveDirection;

    public float radaZone = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ground = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        ScanningRada();
    }
    private void ScanningRada()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radaZone, LayerMask.GetMask("Feed"));
        if (colliders.Length > 0)
        {
            Collider nearest = colliders[0];
            Vector3 direction = nearest.transform.position - transform.position; // hướng từ mình -> target
            direction.y = 0; // bỏ trục Y để chỉ quay trong mặt phẳng ngang

            if (direction.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            Debug.Log("Found Target");
            Debug.Log("current rotation: " + transform.rotation.eulerAngles); // đổi sang Euler cho dễ đọc
            Debug.Log("direction to target: " + direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radaZone);
    }
}
