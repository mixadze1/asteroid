using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform _vectorAsteroidMove;
    [SerializeField] private SpaceShipFactory _shipFactory;
    [SerializeField] private SpaceShipType _type;
    [SerializeField] private GameScenario _scenario;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private ShellFactory _warFactory;
    [SerializeField] private List<Transform> _spawnCoord = new List<Transform>();
    [SerializeField, Range(1f, 40f)] private float _timeToSpawnNlo = 5f;
    [SerializeField, Range(1f, 15f)] private float _prepareTime = 10f;
   
    private GameScenario.State _activateScenarioAsteroid;
    private PlayerShoot _playerShoot;

    private Coroutine _prepareRoutine;
    private Coroutine _prepareRoutineNlo;

    private GameBehaviorCollection _asteroid = new GameBehaviorCollection();
    private GameBehaviorCollection _nonEnemies = new GameBehaviorCollection();
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
            if (!_activateScenarioAsteroid.Progress(_asteroid) && _asteroid.IsEmpty)
            {
                //win
            }
            _asteroid.GameUpdate();
        }
      
    }
    
    private void BeginNewGame()
    {
        _scenarioInProcess = false;
        if (_prepareRoutine != null)
        {
            StopCoroutine(_prepareRoutine);
        }
        CreatePlayer();
       StartCoroutine( CreateNlo());
    }

    private void CreatePlayer()
    {
        SpaceShip spaceShip = _shipFactory.Get(_type);
        spaceShip.SpawnOn();
    }

    private IEnumerator CreateNlo()
    {
        yield return new WaitForSeconds(_timeToSpawnNlo);
        Enemy enemy = _enemyFactory.Get(EnemyType.NLO);
        enemy.SpawnOn(_spawnCoord[Random.Range(0, _instance._spawnCoord.Count - 1)]);
    }

    public static Shell SpawnShell()
    {
        Shell shell = _instance._warFactory.Shell;
        _instance._nonEnemies.Add(shell);
        return shell;
    }

    public static void SpawnAsteroid(AsteroidFactory factory, AsteroidType type)
    {
        Transform transforms = _instance._spawnCoord[Random.Range(0,_instance._spawnCoord.Count - 1)];
        Asteroid asteroid = factory.Get(type);
        asteroid.SpawnOn(transforms, _instance._vectorAsteroidMove);
       _instance._asteroid.Add(asteroid); 
      
    }

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);

        _activateScenarioAsteroid = _scenario.Begin();
        _scenarioInProcess = true;
    }
}
