using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtMouse : MonoBehaviour
{
    [SerializeField] private Transform _needRotation;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BackStep _backStep;
    [SerializeField] private LayerMask _aimLayerMask;
    [SerializeField] private LayerMask _player;

    void Update()
    {
        AimLookAtMouse();
    }
    private void AimLookAtMouse()
    {
        if (_playerMovement.IsFinishing)
        {
            transform.LookAt(_backStep.Enemy.transform);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (CheckMouseInRangeModel(ray))
            return;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            CalculateRotation(hitInfo);         
        }

    }

    private void CalculateRotation(RaycastHit hitInfo)
    {
        var destination = hitInfo.point;
        destination.y = _needRotation.transform.position.y;
        var _direction = destination - _needRotation.transform.position;
        _direction.y = 0f;
        _direction.Normalize();
        _rigidbody.MoveRotation(Quaternion.LookRotation(_direction, _needRotation.transform.up));
       // transform.rotation = Quaternion.LookRotation(_direction, _needRotation.transform.up);
    }

    private bool CheckMouseInRangeModel(Ray ray)
    {
        if (Physics.Raycast(ray, Mathf.Infinity, _player))
            return true;
        return false;
    }
    
}

