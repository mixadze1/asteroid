using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehavior
{
    [SerializeField] private Transform _model;

    private Vector3 _startPosition;
    private float _directionAngleFrom, _directionAngleTo;
    private float _pathOffset;
    private float _speed;
  
    private Transform _moveTo;
    public float Scale { get; private set; }
    public float Health { get; private set; }
    public float Damage { get; private set; }
    public AsteroidFactory OriginFactory { get; set; }

    public AsteroidType Type { get; set; }

    public void Initialize(float scale, float pathOffset, float speed, float health)
    {
        _model.localScale = new Vector3(scale, scale, scale);
        _pathOffset = pathOffset;
        _speed = speed;
        Scale = scale;
        Health = health;
    }

    public void SpawnOn(Transform position, Transform asteroidMove)
    {
        _model.localPosition = position.position;
        _moveTo = asteroidMove;
        _startPosition = _model.localPosition;
    }   

    public override bool GameUpdate()
    {
        if (Health <= 0)
        {
            if (Type == AsteroidType.Large)
            {

            }
            if (Type == AsteroidType.Medium)
            {

            }
            if (Type == AsteroidType.Small)
            {
                Recycle();
            }
            return false;
        }
       
        MoveAsteroid();
        return true;
    }


    private void MoveAsteroid()
    {
        if(_startPosition.x < 0 && _startPosition.y > 0)
        {
            transform.position +=  new Vector3(0.05f, -0.05f, 0) * 0.1f * _speed;
        }

        if(_startPosition.x < 0 && _startPosition.y < 0)
        {
            transform.position += new Vector3(0.05f, 0.05f, 0) * 0.1f * _speed;
        }

        if (_startPosition.x > 0 && _startPosition.y < 0)
        {
            transform.position += new Vector3(-0.05f, 0.05f, 0) * 0.1f * _speed;
        }

        if (_startPosition.x > 0 && _startPosition.y > 0)
        {
            transform.position -=  new Vector3(-0.05f, -0.05f, 0) * 0.1f * _speed;
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

        
