using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtdMouse : MonoBehaviour
{
    [SerializeField] private Transform _needRotation;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BackStep _backStep;
    [SerializeField] private LayerMask _aimLayerMask;

    void Update()
    {
        AimLookAtMouse();
    }
    private void AimLookAtMouse()
    {
        if (_playerMovement.IsFinishing)
            return;

       if (_backStep.CanBackStep && _backStep.Enemy != null)
        {
            transform.LookAt(_backStep.Enemy.transform);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var destination = hitInfo.point;
            destination.y = _needRotation.transform.position.y;

            var _direction = destination - _needRotation.transform.position;
            _direction.y = 0f;
            _direction.Normalize();
            Debug.DrawRay(_needRotation.transform.position, _direction, Color.green);
            _needRotation.transform.rotation = Quaternion.LookRotation(_direction, _needRotation.transform.up);
        }
    }
}

