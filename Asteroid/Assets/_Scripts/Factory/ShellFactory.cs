using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShellFactory : GameObjectFactory
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private Shell _shellEnemyPrefab;
  /*  public Shell Shell => Get(_shellPrefab);
    public Shell ShellEnemy => Get(_shellEnemyPrefab);*/


    public Shell Get(Shell prefab, List<Shell> pool, ShellType type)
    {           
        Shell oldShell = CheckFreeInPool(pool, type);
        if (oldShell)
            return oldShell;

            Shell instance = CreateGameObjectInstance(prefab);
            instance.Type = type;
            pool.Add(instance);
            return instance;    
    }

    public Shell CheckFreeInPool(List<Shell> pool, ShellType type)
    {
        foreach (Shell oldShell in pool)
        {
            
            if (pool == null)
                return null; 

                
            if (oldShell.Type == type && !oldShell.gameObject.activeSelf)
            {
                
                oldShell.gameObject.SetActive(true);
                return oldShell;
            }
        }
        return null;
    }  

    public void Reclaim(Shell shell)
    {
        shell.gameObject.SetActive(false) ;
    }
}

public enum ShellType
{
    Enemy,
    SpaceShip
}