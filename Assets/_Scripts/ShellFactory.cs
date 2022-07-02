using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShellFactory : GameObjectFactory
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private Shell _shellEnemyPrefab;
    public Shell Shell => Get(_shellPrefab);
    public Shell ShellEnemy => Get(_shellEnemyPrefab);

    private T Get<T>(T prefab) where T : GameBehavior
    {
            T instance = CreateGameObjectInstance(prefab);
            return instance;    
    }
    public void Reclaim(GameBehavior entity)
    {
        Destroy(entity.gameObject);
    }
}

public enum ShellType
{
    Enemy,
    SpaceShip
}