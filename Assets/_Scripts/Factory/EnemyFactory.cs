using System;
using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [Serializable]
    class EnemyConfig
    {
        public Enemy Prefab;
        [FloatRangeSlider(0.5f, 2f)]
        public FloatRange Scale = new FloatRange(1f);
        [FloatRangeSlider(-0.4f, 0.4f)]
        public FloatRange PathOffset = new FloatRange(0);
        [FloatRangeSlider(0.001f, 0.005f)]
        public FloatRange Speed = new FloatRange(0);
        [FloatRangeSlider(1f, 10f)]
        public FloatRange Health = new FloatRange(0);
        [FloatRangeSlider(1f, 5f)]
        public FloatRange TimeLiveShell = new FloatRange(0);
    }

    [SerializeField] private EnemyConfig _nlo;

    public Enemy Get(EnemyType type)
    {
        var config = GetConfig(type);
        Enemy instance = CreateGameObjectInstance(config.Prefab);
        instance.OriginFactory = this;
        instance.Initialize(config.Scale.RandomValueInRange, config.PathOffset.RandomValueInRange,
            config.Speed.RandomValueInRange, config.Health.RandomValueInRange, config.TimeLiveShell.RandomValueInRange);
        return instance;
    }

    private EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.NLO:
                return _nlo;
        }
        return _nlo;
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
