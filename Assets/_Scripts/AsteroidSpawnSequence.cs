using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AsteroidSpawnSequence
{
    [SerializeField] private AsteroidFactory _factory;
    [SerializeField] private AsteroidType _type;
    [SerializeField, Range(0f, 100f)] private int _amount = 1;
    [SerializeField, Range(0f, 10f)] private float _cooldown = 1f;

    public struct State
    {
        private AsteroidSpawnSequence _sequence;
        private int _count;
        private float _cooldown;
        public State(AsteroidSpawnSequence sequence)
        {
            _sequence = sequence;
            _count = 0;
            _cooldown = sequence._cooldown;
        }
        public float Progress(float deltaTime)
        {
            _cooldown += deltaTime;
            while (_cooldown > _sequence._cooldown)
            {
                _cooldown -= _sequence._cooldown;
                if (_count >= _sequence._amount)
                {
                    return _cooldown;
                }
                _count++;
                Game.SpawnAsteroid(_sequence._factory, _sequence._type);
            }
            return -1f;
        }
    }

    public State Begin() => new State(this);

}