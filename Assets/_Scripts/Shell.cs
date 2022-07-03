using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : GameBehavior
{
    [SerializeField] private TargetPointCheckEnemy _targetPoint;

    private Transform _target;
    private SpaceShip _ship;

    private Vector3 _positionShip;

    private float _speed;
    private float _timeLiveShell;

    private float _correctSpeed = 0.1f;
      
    private bool _isPlayer;
   
    public ShellFactory OriginFactory { get; set; }

    public void  Initialize(Transform spawn, float speed, float damage, bool isPlayer, float timeLiveShell)
    {  
        transform.localPosition = spawn.position;
        transform.rotation = spawn.rotation;
        _speed = speed;
        _isPlayer = isPlayer;
        _timeLiveShell = timeLiveShell;
        if (!isPlayer)
            FindPlayer();
        StartCoroutine(TimeLiveShell());
    }

    private void Update()
    {
        if (_isPlayer)
        transform.Translate(transform.up * _speed * _correctSpeed, Space.World);

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


    private IEnumerator TimeLiveShell()
    {
        yield return new WaitForSeconds(_timeLiveShell);
        Destroy(gameObject);
    }
    public override void Recycle()
    {
        if (this.gameObject != null)
            OriginFactory.Reclaim(this);
    }
}
