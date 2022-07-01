using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : GameBehavior
{
    private Vector3 _launchPoint, _targetPoint, _launchVelocity;
    private float _speed;

    public ShellFactory OriginFactory { get; set; }

    public void  Initialize(Transform spawn, float speed)
    {
        transform.localPosition = spawn.position;
        transform.rotation = spawn.rotation;
        _speed = speed;
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
