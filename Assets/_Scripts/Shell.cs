using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : GameBehavior
{
    [SerializeField] private TargetPointCheckEnemy _targetPoint;
    private float _speed;

    public ShellFactory OriginFactory { get; set; }

    public void  Initialize(Transform spawn, float speed, float damage)
    {
        transform.localPosition = spawn.position;
        transform.rotation = spawn.rotation;
        _speed = speed;
        _targetPoint.Init(damage);
        
    }

    private void Update()
    {
        transform.Translate(transform.up * _speed, Space.World);
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
}
}
