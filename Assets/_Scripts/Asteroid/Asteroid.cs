using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehavior
{
    [SerializeField] private Transform _model;
    [SerializeField] private Transform _explosion;

    private Vector3 _startPosition;

    private float _moveX = 0.05f;
    private float _moveY = 0.05f;
    private float _correctSpeed = 0.1f;
    private float _speed;

    private float _bigExplosion = 1f;
    private float _mediumExposion = 0.75f;
    private float _smallExplosion = 0.5f;
  
    private Transform _moveTo;
    public float Scale { get; private set; }
    public float Health { get; private set; }
    public float Damage { get; private set; }
    public AsteroidFactory OriginFactory { get; set; }

    public AsteroidType Type { get; set; }

    public void Initialize(float scale, float speed, float health)
    {
        _model.localScale = new Vector3(scale, scale, scale);
        _speed = speed;
        Scale = scale;
        Health = health;

    }

    public void SpawnOn(Vector3 position, Transform asteroidMove, Vector3 startPosition)
    {
        _model.localPosition = position;
        _moveTo = asteroidMove;
        _startPosition = startPosition;
    }   

    public override bool GameUpdate()
    {
        if (Health <= 0)
        {
            SfxAudio._instance.DieAsteroid.Play();
            if (Type == AsteroidType.Large)
            {
                Recycle();
                SpawnMediumAsteroid();
                Explosion(_bigExplosion);
                GUIManager._instance.Score += GUIManager._instance.LargeAsteroidScore;
            }
            if (Type == AsteroidType.Medium)
            {
                Recycle();
                SpawnSmallAsteroid();
                Explosion(_mediumExposion);
                GUIManager._instance.Score += GUIManager._instance.MediumAteroidScore;
            }
            if (Type == AsteroidType.Small)
            {
                Recycle();
                Explosion(_smallExplosion);
                GUIManager._instance.Score += GUIManager._instance.SmallAteroidScore;
               
            }
            return false;
        }
        MoveAsteroid();
        return true;
    }

    private void SpawnMediumAsteroid()
    {    
        Game.SpawnAsteroidAfterDieBigAsteroid(this.transform.position, AsteroidType.Medium, _startPosition);
    }

    private void SpawnSmallAsteroid()
    {   
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Game.SpawnAsteroidAfterDieBigAsteroid(this.transform.position, AsteroidType.Small, _startPosition);
    }

    private void Explosion(float scale)
    {
        _explosion.localScale = Vector3.one * scale;
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }

    private void MoveAsteroid()
    {
        if(_startPosition.x < 0 && _startPosition.y > 0)
        {
            MoveLeftDown();
        }

        if(_startPosition.x < 0 && _startPosition.y < 0)
        {
            MoveLeftUp();
        }

        if (_startPosition.x > 0 && _startPosition.y < 0)
        {
            MoveRightUp();
        }

        if (_startPosition.x > 0 && _startPosition.y > 0)
        {
            MoveRightDown();
        }     
    }

    public void MoveLeftUp()
    {   
        transform.position += new Vector3(_moveX, _moveY, 0) * _correctSpeed * _speed;
    }

    public void MoveLeftDown()

    {
        transform.position += new Vector3(_moveX, -_moveY, 0) * _correctSpeed * _speed;
    }

    public void MoveRightUp()
    {
        transform.position += new Vector3(-_moveX, _moveY, 0) * _correctSpeed * _speed;
    }
    public void MoveRightDown()
    {
        transform.position -= new Vector3(-_moveX, -_moveY, 0) * _correctSpeed * _speed;
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

        

