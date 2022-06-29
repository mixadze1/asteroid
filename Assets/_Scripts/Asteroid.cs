using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Transform _model;
    private float _directionAngleFrom, _directionAngleTo;
    private float _pathOffset;
    private float _speed;
    
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

    public void SpawnOn()
    {

    }

    private void DieAsteroid()
    {
        if (Type == AsteroidType.Large)
        {

        }
        if (Type == AsteroidType.Medium)
        {

        }
        if (Type == AsteroidType.Small)
        {
            Reclaim();
        }
    }

    public void Reclaim()
    {
        OriginFactory.Reclaim(this);
    }
        
}

        

