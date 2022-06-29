using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    
    private SpaceShip _playerMove;
    private PlayerShoot _playerShoot;
    private List<Asteroid> _asteroid;

    private Coroutine _prepareRoutine;

    private void Start()
    {
        BeginNewGame();
    }

    private void FixedUpdate()
    {
        _playerMove.PhysicsUpdate();
    }
    
    private void BeginNewGame()
    {
        if (_prepareRoutine != null)
        {
            StopCoroutine(_prepareRoutine);
        }
    }

    private void CreatePlayer()
    {
       //_playerMove = Instantiate(_prefab, _spawnCoord.position, Quaternion.identity).GetComponent<SpaceShip>();
    }

    public static void SpawnAsteroid(AsteroidFactory factory, AsteroidType type)
    { 
        Asteroid asteroid = factory.Get(type);
        asteroid.SpawnOn();
    }
}
