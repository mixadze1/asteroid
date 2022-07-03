using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehavior
{
    [SerializeField] private Transform _spawnShellCoord;
    private float _speedShell = 8f;
    private Transform _model;
    private float _timeToShoot = 3f;
    private float _speed;
    private float frequency = 400f;
    private float offset = 3.5f;
    private float _damage = 1f;
    private float _timeLiveShell;
    private Vector3 _startPosition;
    private SpaceShip _target;
    public float Health { get; private set; }

    public EnemyFactory OriginFactory { get; set; }

    public void Initialize(float scale, float pathOffset, float speed, float health, float timeLiveShell)
    {
        _timeLiveShell = timeLiveShell;
        _model = this.transform;
        _model.localScale = new Vector3(scale, scale, scale);
        _speed = speed;
        Health = health;
    }

    public void SpawnOn(Transform transform)
    {
        _model.localPosition = transform.localPosition;
        _startPosition = transform.position ;
        StartCoroutine(Shoot());
    }

    public void Update()
    {
        if(Health <= 0)
        {
            GUIManager._instance.Score += GUIManager._instance.NloScore;
            Recycle();
        }
        if (_startPosition.x < 0)
            MoveNlo(_speed);
        else
            MoveNlo(- _speed);

    }

    private void MoveNlo(float speed)
    {
        transform.position = new Vector3(transform.localPosition.x + speed, Mathf.Sin(Time.fixedTime) + offset, transform.localPosition.z);
    }


    private IEnumerator Shoot()
    {while(true)
        {
            Debug.Log("tut");
            yield return new WaitForSeconds(_timeToShoot);
            Game.SpawnShell(false)?.Initialize(_spawnShellCoord, _speedShell, _damage, false, _timeLiveShell);
        }
       
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}
