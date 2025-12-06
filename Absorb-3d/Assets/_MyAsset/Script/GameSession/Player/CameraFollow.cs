using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float yOffset = 5f;
    private void Follow()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.y += yOffset;
        newPosition.z = player.transform.position.z;
        newPosition.x = player.transform.position.x;
        transform.position = newPosition;
    }
    private void Update()
    {
        if(player != null)
            Follow();
    }
}
