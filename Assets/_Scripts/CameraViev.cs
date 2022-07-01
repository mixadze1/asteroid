using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViev : MonoBehaviour
{


    public void LateUpdate()
    {
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(point);
        if (point.x < 0)
        {
            point.x = 1;
            Debug.Log("tut");
            TeleportXZero(point);
        }

        if (point.x > Screen.width)
        {
            Debug.Log("zdec");
            point.x = 0;
            Teleport(point);

        }
        if (point.y < 0)
        {
            point.y = 1;
            TeleportYZero(point);
            return;
        }
        if (point.y > Screen.height)
        {
            point.y = 0;
            Teleport(point);
            return;
        }

    }

    private void TeleportXZero(Vector3 point)
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(point);
        world.z = 0;
        world.x = -world.x;
        transform.localPosition = world;

    }

    private void TeleportYZero(Vector3 point)
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(point);
        world.z = 0;
        world.y = -world.y;
        transform.localPosition = world;
    }


    private void Teleport(Vector3 point)
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(point);
        world.z = 0;
        transform.localPosition = world;
    }
}
