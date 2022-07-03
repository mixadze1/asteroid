using System;
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
    }

    [SerializeField] private AsteroidConfig _small, _medium, _large;

    public Asteroid Get(AsteroidType type)
    {
        var config = GetConfig(type);
        Asteroid instance = CreateGameObjectInstance(config.Prefab);
        instance.OriginFactory = this;
        instance.Type = type;
        instance.Initialize(config.Scale.RandomValueInRange,config.Speed.RandomValueInRange,
            config.Health.RandomValueInRange);
        return instance;
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
    public void Reclaim(Asteroid enemy)
    {
        Destroy(enemy.gameObject);
    }
}

