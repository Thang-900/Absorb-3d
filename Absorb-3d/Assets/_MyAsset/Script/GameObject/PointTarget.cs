using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointTarget : MonoBehaviour
{
    private Transform ground;

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
        Collider[] collider = Physics.OverlapSphere(ground.position, radaZone, LayerMask.GetMask("Feed"));


        if (collider.Length > 0)
        {
            Collider neareast = collider[0];
            Vector3 neareastPos = transform.position - neareast.transform.position;
            neareastPos.y = 0;
            Debug.Log("Found Target");
            transform.rotation = Quaternion.LookRotation(neareastPos);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radaZone);
    }
}
