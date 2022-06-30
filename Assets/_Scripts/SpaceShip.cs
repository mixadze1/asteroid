using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : GameBehavior
{
    [SerializeField] private Transform _model;
    private float _speedMax;
    private float _speed;
    private float _speedRotate;
    private float _pathOffset;
    private float _inertia;
    private float _speedDamping;

    private float _currentSpeed;

    private Vector3 _rememberVector;

    private bool _isImmortal;
    private bool _isStartGame = true;

    public float Scale { get; private set; }
    public float Health { get; private set; }
    public float Damage { get; private set; }
  

    public SpaceShipFactory OriginFactory { get; set; }
    public void Initialize(float rotate,float scale, float speed,float inertia, float health, float damage, float speedDamping)
    {
        _speedRotate = rotate;
        _model.localScale = new Vector3(scale, scale, scale);
        _speed = speed;
        _inertia = inertia;
        Health = health;
        Scale = scale;
        Damage = damage;
        _speedDamping = speedDamping;
    }

    public void SpawnOn()
    {
        _model.localPosition = new Vector3(0,0,0);
    }
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            AccelerationShip();
        }
        else if (_isStartGame)
        {
            return;
        }
        else if (!_isStartGame)
        {
            MoveInertia();
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateShip(_speedRotate);
        }

        if (Input.GetKey(KeyCode.D))
        {
            RotateShip(-_speedRotate);
        }
    }
    public void PhysicsUpdate()
    {
        

    }
    private void MoveInertia()
    {
        transform.Translate(_rememberVector * _inertia, Space.World);
    }

    private void AccelerationShip()
    {
        _isStartGame = false;
        transform.Translate(transform.up * _speed, Space.World);
        _rememberVector = transform.up;
    }

    private void RotateShip(float sideRotate)
    {
        this.transform.localRotation *= Quaternion.Euler(0f, 0f, sideRotate);
    }

    public override void Recycle()
    {
        throw new System.NotImplementedException();
    }
}

