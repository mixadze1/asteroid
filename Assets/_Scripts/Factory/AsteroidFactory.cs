using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AsteroidFactory : GameObjectFactory
{
    [Serializable]
    class AsteroidConfig
    {
        public Asteroid Prefab;
        [FloatRangeSlider(0.5f, 2f)]
        public FloatRange Scale = new FloatRange(1f);
        [FloatRangeSlider(-0.4f, 0.4f)]
        public FloatRange PathOffset = new FloatRange(0);
        [FloatRangeSlider(0.2f, 5f)]
        public FloatRange Speed = new FloatRange(0);
        [FloatRangeSlider(1f, 10f)]
        public FloatRange Health = new FloatRange(0);
        [FloatRangeSlider(0.2f, 1f)]
        public FloatRange Explosion = new FloatRange(0);
        [FloatRangeSlider(-360f, 360f)]
        public FloatRange Rotation = new FloatRange(0);
    }

    [SerializeField] private AsteroidConfig _small, _medium, _large;


    public Asteroid Get(AsteroidType type, List<Asteroid> pool)
    {
        Asteroid fromPoolAsteroid = CheckFreeAsteroid(type, pool);
        if (fromPoolAsteroid)
        {
            var oldConfig = GetConfig(type);
            fromPoolAsteroid.Initialize(oldConfig.Scale.RandomValueInRange, oldConfig.Speed.RandomValueInRange,
                oldConfig.Health.RandomValueInRange, oldConfig.Explosion.RandomValueInRange, oldConfig.Rotation.RandomValueInRange);
            return fromPoolAsteroid;
        }

        return NewAsteroid(type, pool);
    }

    private Asteroid NewAsteroid(AsteroidType type, List<Asteroid> pool)
    {
        var config = GetConfig(type);
        Asteroid instance = CreateGameObjectInstance(config.Prefab);
        instance.Type = type;
        instance.OriginFactory = this;
        instance.Initialize(config.Scale.RandomValueInRange, config.Speed.RandomValueInRange,
            config.Health.RandomValueInRange, config.Explosion.RandomValueInRange, config.Rotation.RandomValueInRange);
        pool.Add(instance);
        return instance;
    }

    private Asteroid CheckFreeAsteroid(AsteroidType type, List<Asteroid> pool)
    {
        foreach (var asteroid in pool)
        {
            if (pool == null)
                return null;
            if (asteroid.Type == type && !asteroid.gameObject.activeSelf)
            {             
                asteroid.gameObject.SetActive(true);
                return asteroid;
            }
        }
        return null;
    }

    private AsteroidConfig GetConfig(AsteroidType type)
    {
        switch (type)
        {
            case AsteroidType.Large:
                return _large;
            case AsteroidType.Small:
                return _small;
            case AsteroidType.Medium:
                return _medium;
        }
        return _medium;
    }
    public void Reclaim(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
    }
}

