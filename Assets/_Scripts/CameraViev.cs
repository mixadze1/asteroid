using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViev : MonoBehaviour
{
    //private Camera _camera;
   [SerializeField] private Transform _model;

    public void Init(Transform model)
    {
        _model = model;
        //_camera = camera;
    }

    public void LateUpdate()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position);
        if (point.x < 0)
        {
            Debug.Log("tut");
            point.x = 1;
            Teleport(point);
        }
            
        if (point.x > 1)
        {
            point.x = 0;
            Teleport(point);
        }
        if (point.y < 0)
        { 
            point.y = 1;
            Teleport(point);
        }
        if (point.y > 1)
        {
            point.y = 0;
            Teleport(point);
        }

    }

    private void Teleport(Vector3 point)
    {
        Debug.Log(point);
       transform.localPosition = Camera.main.WorldToViewportPoint(point);
    }

    
}
