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
  
    private float _health;
    private float _speed;
    private float _explosionSize;
    private float _correctRotationZ;
    private float _rotationZ;

    public AsteroidFactory OriginFactory { get; set; }

    public AsteroidType Type { get; set; }

    public void Initialize(float scale, float speed, float health, float explosion, float rotation)
    {
        _model.localScale = new Vector3(scale, scale, scale);
        _speed = speed;
        _health = health;
        _explosionSize = explosion;
        _rotationZ = rotation;
    }

    public void SpawnOn(Vector3 position, float RotationZ, Vector3 startPosition, float correctRotation)
    {
        _model.localPosition = position;
        _startPosition = startPosition;
        _correctRotationZ = correctRotation;
        if (RotationZ != 0)
            _rotationZ = RotationZ;
    }   

    public override bool GameUpdate()
    {
        if (_health <= 0)
        {
            SfxAudio._instance.DieAsteroid.Play();
            if (Type == AsteroidType.Large)
            {
                Recycle();
                SpawnMediumAsteroid();
                Explosion(_explosionSize);
                GUIManager._instance.Score += GUIManager._instance.LargeAsteroidScore;
            }
            if (Type == AsteroidType.Medium)
            {
                Recycle();
                SpawnSmallAsteroid();
                Explosion(_explosionSize);
                GUIManager._instance.Score += GUIManager._instance.MediumAteroidScore;
            }
            if (Type == AsteroidType.Small)
            {
                Recycle();
                Explosion(_explosionSize);
                GUIManager._instance.Score += GUIManager._instance.SmallAteroidScore;
            }
            return false;
        }
        MoveAsteroid();
        return true;
    }

    private void SpawnMediumAsteroid()
    {    
        Game.SpawnAsteroidAfterDieAsteroid(_rotationZ, AsteroidType.Medium, _startPosition, this.transform.position);
    }

    private void SpawnSmallAsteroid()
    {   
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Game.SpawnAsteroidAfterDieAsteroid(_rotationZ, AsteroidType.Small, _startPosition, this.transform.position);
    }

    private void Explosion(float scale)
    {
        _explosion.localScale = Vector3.one * scale;
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }

    private void MoveAsteroid()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rotationZ + _correctRotationZ);    
        transform.position += transform.up * Time.deltaTime * _speed;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}