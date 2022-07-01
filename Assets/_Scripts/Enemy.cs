using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehavior
{
    private Transform _model;
    private float _pathOffset;
    private float _speed;
    private float frequency = 400f;
    private float offset = 3.5f;
    private Vector3 _startPosition;
    public float Health { get; private set; }

    public EnemyFactory OriginFactory { get; set; }
    public void Initialize(float scale, float pathOffset, float speed, float health)
    {
        _model = this.transform;
        _model.localScale = new Vector3(scale, scale, scale);
        _pathOffset = pathOffset;
        _speed = speed;
        Health = health;
    }

    public void SpawnOn(Transform transform)
    {
        Debug.Log(transform.position);
        _model.localPosition = transform.localPosition;
        _startPosition = transform.position ;
    }

    public void Update()
    {
        if(Health <= 0)
        {
            Recycle();
        }
        if (_startPosition.x < 0)
            transform.position = new Vector3(transform.localPosition.x + 0.005f, Mathf.Sin(Time.fixedTime) * 1f + offset, transform.localPosition.z);
        else
            transform.position = new Vector3(transform.localPosition.x - 0.005f, Mathf.Sin(Time.fixedTime) * 1f - offset, transform.localPosition.z);

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
