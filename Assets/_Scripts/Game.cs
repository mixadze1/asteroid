using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private AsteroidFactory _asteroidFactory;
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

    private Coroutine _prepareRoutine;
    private Coroutine _prepareRoutineNlo;

    private GameBehaviorCollection _asteroid = new GameBehaviorCollection();
    private GameBehaviorCollection _nonEnemies = new GameBehaviorCollection();
    private bool _isGetReady = true;
    private bool _scenarioInProcess;
    private bool _isNotLose;
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
    {while(!_isNotLose)
        {
            yield return new WaitForSeconds(_instance._timeToSpawnNlo);
            Enemy enemy = _instance._enemyFactory.Get(EnemyType.NLO);
            enemy.SpawnOn(_instance._spawnCoord[Random.Range(0, _instance._spawnCoord.Count - 1)]);
        }
       
    }

    public static Shell SpawnShell()
    {
        Shell shell = _instance._warFactory.Shell;
        _instance._nonEnemies.Add(shell);
        return shell;
    }

    public static void SpawnAsteroid(AsteroidFactory factory, AsteroidType type)
    {
        Vector3 position = _instance._spawnCoord[Random.Range(0,_instance._spawnCoord.Count - 1)].position;
        Asteroid asteroid = factory.Get(type);
        asteroid.SpawnAsteroid(position, _instance._vectorAsteroidMove);
       _instance._asteroid.Add(asteroid); 
    }

    public static void SpawnAsteroidAfterDieBigAsteroid(Vector3 position, AsteroidType type)
    {
        Asteroid asteroid = _instance._asteroidFactory.Get(type);
        Asteroid asteroid2 = _instance._asteroidFactory.Get(type);

        asteroid.SpawnAsteroid(position, _instance._vectorAsteroidMove);
        asteroid2.SpawnAsteroid(position, _instance._vectorAsteroidMove);

        _instance._asteroid.Add(asteroid);
        _instance._asteroid.Add(asteroid2);
    }

    private IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(_prepareTime);

        _activateScenarioAsteroid = _scenario.Begin();
        _scenarioInProcess = true;
    }
}
