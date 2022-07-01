using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : GameBehavior
{
    [SerializeField] private TargetPointCheckEnemy _targetPoint;
    private float _speed;
    private Color _color;
    private Transform _target;

    public ShellFactory OriginFactory { get; set; }

    public void  Initialize(Transform spawn, float speed, float damage, Color color, Transform target)
    {
        _color = color;
        transform.localPosition = spawn.position;
        transform.rotation = spawn.rotation;
        _speed = speed;
        _targetPoint.Init(damage);
        _target = target;
        if (_target != null)
        {
            ShootPlayer();
        }
    }

    private void Update()
    {
        if (_target == null)
            transform.Translate(transform.up * _speed, Space.World);

    }

    private void ShootPlayer()
    {
        //_model.transform.position.Lerp(transform.position, _target.transform.position, 0.5f);
    }

    public override void Recycle()
    {
        
        OriginFactory.Reclaim(this);
}
}
