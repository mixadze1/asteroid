using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehavior
{
    [SerializeField] private Transform _model;
    [SerializeField] private Transform _explosion;

    private Vector3 _startPosition;
    private float _directionAngleFrom, _directionAngleTo;
    private float _pathOffset;
    private float _moveX = 0.05f;
    private float _moveY = 0.05f;
    private float _correctSpeed = 0.1f;
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

    public void SpawnAsteroid(Vector3 position, Transform asteroidMove, Vector3 startPosition)
    {
        _model.localPosition = position;
        _moveTo = asteroidMove;
        _startPosition = startPosition;
    }   

    public override bool GameUpdate()
    {
        if (Health <= 0)
        {
            if (Type == AsteroidType.Large)
            {
                Recycle();
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Game.SpawnAsteroidAfterDieBigAsteroid(this.transform.position, AsteroidType.Medium, _startPosition);
                GUIManager._instance.Score += GUIManager._instance.LargeAsteroidScore;
            }
            if (Type == AsteroidType.Medium)
            {
                Recycle();
                _explosion.localScale = Vector3.one * 0.75f;
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Game.SpawnAsteroidAfterDieBigAsteroid(this.transform.position, AsteroidType.Small, _startPosition);
                GUIManager._instance.Score += GUIManager._instance.MediumAteroidScore;
            }
            if (Type == AsteroidType.Small)
            {
                _explosion.localScale = Vector3.one * 0.5f;
                Instantiate(_explosion, transform.position, Quaternion.identity);
                GUIManager._instance.Score += GUIManager._instance.SmallAteroidScore;
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
            transform.position +=  new Vector3(_moveX, -_moveY, 0) * _correctSpeed * _speed;
        }

        if(_startPosition.x < 0 && _startPosition.y < 0)
        {
            transform.position += new Vector3(_moveX, _moveY, 0) * _correctSpeed * _speed;
        }

        if (_startPosition.x > 0 && _startPosition.y < 0)
        {
            transform.position += new Vector3(-_moveX, _moveY, 0) * _correctSpeed * _speed;
        }

        if (_startPosition.x > 0 && _startPosition.y > 0)
        {
            transform.position -=  new Vector3(-_moveX, -_moveY, 0) * _correctSpeed * _speed;
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

        

