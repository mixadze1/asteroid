using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameScenario _scenario;
    [SerializeField] private List<Transform> _spawnCoord = new List<Transform>();
    [SerializeField, Range(1f, 15f)] private float _prepareTime = 10f;
   
    private GameScenario.State _activateScenario;
    private SpaceShip _playerMove;
    private PlayerShoot _playerShoot;

    private Coroutine _prepareRoutine;

    private GameBehaviorCollection _asteroid = new GameBehaviorCollection();
    private bool _isGetReady = true;
    private bool _scenarioInProcess;

    public static Game _instance;

    private void OnEnable()
    {
        _instance = this;
    }

    private void Start()
    {
        BeginNewGame();
    }
    private void Update()
    {
        if (_isGetReady)
        {
            _prepareRoutine = StartCoroutine(PrepareRoutine());
            _isGetReady = false;
        }
        if (_scenarioInProcess)
        {
            if (!_activateScenario.Progress() && _asteroid.IsEmpty)
            {
                //win
            }
            _asteroid.GameUpdate();
        }
      
    }
    private void FixedUpdate()
    {
        //_playerMove.PhysicsUpdate();
    }
    
    private void BeginNewGame()
    {
        _scenarioInProcess = false;
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
        Transform transforms = _instance._spawnCoord[Random.Range(0,_instance._spawnCoord.Count - 1)];
        Asteroid asteroid = factory.Get(type);
        asteroid.SpawnOn(transforms);
       _instance._asteroid.Add(asteroid); 
      
    }

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);

        _activateScenario = _scenario.Begin();
        _scenarioInProcess = true;
    }
}
