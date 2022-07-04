using System;
using UnityEngine;

[CreateAssetMenu]
public class SpaceShipFactory : GameObjectFactory
{
    [Serializable]
    class SpaceShipConfig
    {
        public SpaceShip Prefab;   
        [Range(0.2f, 1f)]
        public float SpeedMax;
        [Range(0.01f, 1f)]
        public float Speed;
        [Range(1f, 10f)]     
        public float SpeedRotatePlayer;
        [Range(0.01f, 1f)]
        public float SpeedShell;
        [Range(1, 5)]
        public int Health;
        [Range(0.01f, 0.1f)] 
        public float Inertia = 0.03f;
        [Range(1f, 10f)]
        public float Damage;
        [Range(0.1f, 2f)] 
        public float Scale;
        [Range(1f, 5f)]
        public float TimeImmortal;
        [Range(0.1f, 0.5f)]
        public float TimeBlinking;
        [Range(1f, 5f)]
        public float TimeLiveShell;
    }

    [SerializeField] private SpaceShipConfig _whiteSpaceShip, _blackSpacekShip, _fastSpaceShip;

    public SpaceShip Get(SpaceShipType type)
    {
        var config = GetConfig(type);
        SpaceShip instance = CreateGameObjectInstance(config.Prefab);
        instance.OriginFactory = this;
        instance.Initialize(config.SpeedRotatePlayer, config.Scale,config.Speed,
            config.Inertia, config.Health, config.Damage, config.SpeedShell, config.TimeImmortal,
            config.TimeBlinking, config.TimeLiveShell);
        return instance;
    }

    private SpaceShipConfig GetConfig(SpaceShipType type)
    {
        switch (type)
        {
            case SpaceShipType.Fast:
                return _fastSpaceShip;
            case SpaceShipType.White:
                return _whiteSpaceShip;
            case SpaceShipType.Black:
                return _blackSpacekShip;
        }
        return _whiteSpaceShip;
    }

    public void Reclaim(SpaceShip ship)
    {
        if (ship.gameObject != null)
            Destroy(ship.gameObject);
    }
}
