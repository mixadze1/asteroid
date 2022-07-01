using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehavior
{
    [SerializeField] private Transform _spawnShellCoord;
    private float _speedShell = 0.03f;
    private Transform _model;
    private float _timeToShoot = 3f;
    private float _pathOffset;
    private float _speed;
    private float frequency = 400f;
    private float offset = 3.5f;
    private float _damage = 1f;
    private Vector3 _startPosition;
    private SpaceShip _target;
    public float Health { get; private set; }

    public EnemyFactory OriginFactory { get; set; }

    public void Initialize(float scale, float pathOffset, float speed, float health)
    {
        _model = this.transform;
        _model.localScale = new Vector3(scale, scale, scale);
        _pathOffset = pathOffset;
        _speed = speed;
        Health = health;
       _target = FindObjectOfType<SpaceShip>();
    }

    public void SpawnOn(Transform transform)
    {
        _model.localPosition = transform.localPosition;
        _startPosition = transform.position ;
        //StartCoroutine(Shoot());
    }

    public void Update()
    {
        if(Health <= 0)
        {
            GUIManager._instance.Score += 200;
            Recycle();
        }
        if (_startPosition.x < 0)
            transform.position = new Vector3(transform.localPosition.x + 0.005f, Mathf.Sin(Time.fixedTime) * 1f + offset, transform.localPosition.z);
        else
            transform.position = new Vector3(transform.localPosition.x - 0.005f, Mathf.Sin(Time.fixedTime) * 1f - offset, transform.localPosition.z);

    }


    private IEnumerator Shoot()
    {while(true)
        {
            Debug.Log("tut");
            yield return new WaitForSeconds(_timeToShoot);
            Game.SpawnShell()?.Initialize(_spawnShellCoord, _speedShell, _damage, Color.red,_target.transform);
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
