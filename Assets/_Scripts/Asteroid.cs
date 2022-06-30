using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehavior
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
       List<Vector2> position = new List<Vector2>();
        position.Add(new Vector2(-12, 4));
        position.Add ( new Vector3(-12, -5));
        position.Add ( new Vector3(12,4));
        position.Add ( new Vector3(12,-5));
        _model.position = position[Random.Range(0,position.Count - 1)];
    }   

    public override bool GameUpdate()
    {
        Debug.Log("tut");
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

    private void Update()
    {
        _model.transform.position += new Vector3(Random.Range(0.001f, 0.01f), Random.Range(0.001f, 0.01f), Random.Range(0.001f, 0.01f));
    }
    private void MoveAsteroid()
    {
       
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

        

