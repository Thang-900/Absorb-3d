using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnBack : MonoBehaviour
{
    Transform cam;
    bool canChangePosition;
    private void Start()
    {
        cam = gameObject.transform;
        canChangePosition = true;
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(canChangePosition)
            {
                cam.position += new Vector3(0, 0, 26);
                cam.rotation = Quaternion.Euler(22, 180, 0);
                canChangePosition = false;
            }
            
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            if(!canChangePosition)
            {
                cam.position += new Vector3(0, 0, -26);
                cam.rotation = Quaternion.Euler(22, 0, 0);
                canChangePosition = true;
            }
            
        }
    }
}
