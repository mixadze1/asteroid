using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smooth = 5.0f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -5);

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _smooth);
    }

}
