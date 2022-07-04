using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BlasterShot _shot;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _delay = 1f;
   
    private float _nextFireTime;

    private void Update()
    {
        if (ReadyToFire() && Input.GetMouseButton(0))
            Fire();
    }

    private bool ReadyToFire()
    {
        if (_playerMovement.Movement > 0)
            return true;
        return false;
    }

    private void Fire()
    {
        _nextFireTime = Time.time + _delay;
        var shot = Instantiate(_shot, _firePoint.position, Quaternion.Euler(transform.forward));
        shot.Launch(transform.forward);
    }
}
