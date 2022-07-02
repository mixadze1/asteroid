using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : GameBehavior
{
    [SerializeField] private TargetPointCheckEnemy _targetPoint;
    private float _speed;
    private Color _color;
    private Transform _target;
    private SpaceShip _ship;
    private bool _isPlayer;
    private Vector3 _positionShip;
    public ShellFactory OriginFactory { get; set; }

    public void  Initialize(Transform spawn, float speed, float damage, Color color, bool isPlayer)
    {  
        _color = color;
        transform.localPosition = spawn.position;
        transform.rotation = spawn.rotation;
        _speed = speed;
        _isPlayer = isPlayer;
        if (!isPlayer)
            FindPlayer();

    }

    private void Update()
    {
        if (_isPlayer)
        transform.Translate(transform.up * _speed, Space.World);

        if (_ship != null)
        {
            ShellMoveToPlayer();    
        }
       
    }

    private void FindPlayer()
    {
        _ship = FindObjectOfType<SpaceShip>();
        if(_ship != null)
            _positionShip = _ship.transform.position;
    }

    private void ShellMoveToPlayer()
    {
        if (_ship != null)
        {
            transform.rotation = _ship.transform.rotation;
            transform.position += (_positionShip - transform.position).normalized * 8 * Time.deltaTime;
            if ((_positionShip - transform.position).sqrMagnitude < 0.01f) Destroy(gameObject);
        }
    }

    public override void Recycle()
    {
        if (this.gameObject != null)
            OriginFactory.Reclaim(this);
}
}
