using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class Game : MonoBehaviour
{
    [SerializeField] private AsteroidFactory _asteroidFactory;
    [SerializeField] private Transform _vectorAsteroidMove;
    [SerializeField] private SpaceShipFactory _shipFactory;    
    [SerializeField] private GameScenario _scenario;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private ShellFactory _warFactory;
    [SerializeField] private MenuSetting _menuSetting;
    [SerializeField] private SpaceShipType _type;
    [SerializeField] private List<Transform> _spawnCoord = new List<Transform>();
    [SerializeField, Range(1f, 40f)] private float _timeToSpawnNlo = 5f;
    [SerializeField, Range(1f, 15f)] private float _prepareTime = 10f;
   
    private GameScenario.State _activateScenarioAsteroid;

    private Coroutine _prepareRoutine;
    private Coroutine _prepareRoutineNlo;

    private GameBehaviorCollection _nlo = new GameBehaviorCollection();
    private GameBehaviorCollection _asteroid = new GameBehaviorCollection();
    private GameBehaviorCollection _spaceShip = new GameBehaviorCollection();
    private GameBehaviorCollection _shell = new GameBehaviorCollection();

    private bool _isGetReady = true;
    private bool _scenarioInProcess;
    private bool _isNotLose;
    public static Game _instance;
    private GameControl _gameControl;

   public  GameControl GameControl { get => _gameControl; set => _gameControl = value; }

    private void OnEnable()
    {      
        _instance = this;
        GameController();
    }

    private void Start()
    {
        BeginNewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuSetting.ActivateMenu();
        }
        if (_isGetReady)
        { 
            _isGetReady = false;
        }
        if (_scenarioInProcess)
        {
            if (!_activateScenarioAsteroid.Progress(_asteroid) && _asteroid.IsEmpty)
            {
                // TODO win
            }
            _asteroid.GameUpdate();
        }
      
    }

    public void BeginNewGame()
    {
        Time.timeScale = 1;
        _scenarioInProcess = false;
        if (_prepareRoutine != null)
        {
            StopCoroutine(_prepareRoutine);
        }
        if (_prepareRoutineNlo != null)
        {
            StopCoroutine(_prepareRoutineNlo);
        }
        if (_spaceShip != null)
        {
            _spaceShip.Clear();
        }
        _nlo.Clear();
        _asteroid.Clear();
        CreatePlayer(_gameControl);
        _prepareRoutineNlo = StartCoroutine(CreateNlo());
        _prepareRoutine = StartCoroutine(PrepareRoutine());
    }

    private void GameController()
    {
        if (PlayerPrefs.GetInt(Const.GameController.MOUSE) == 0)
            GameControl = GameControl.KeyBoard;
        else
            GameControl = GameControl.Mouse;
    }

    private void CreatePlayer(GameControl gameControl)
    {
            SpaceShip spaceShip = _shipFactory.Get(_type);
            spaceShip.SpawnOn(gameControl);
            _spaceShip.Add(spaceShip);     
    }

    private IEnumerator CreateNlo()
    {
        while(!_isNotLose)
        {
            yield return new WaitForSeconds(_instance._timeToSpawnNlo);
            Enemy enemy = _instance._enemyFactory.Get(EnemyType.NLO);
            enemy.SpawnOn(_instance._spawnCoord[Random.Range(0, _instance._spawnCoord.Count - 1)]);
            _nlo.Add(enemy);
        }     
    }

    public static Shell SpawnShell(bool isPlayerShell)
    {
        if(isPlayerShell)
        {
            Shell shell = _instance._warFactory.Shell;
            _instance._shell.Add(shell);
            return shell;
        }
        else
        {
            Shell shell = _instance._warFactory.ShellEnemy;
            _instance._shell.Add(shell);
            return shell;
        }      
    }

    public static void SpawnAsteroid(AsteroidFactory factory, AsteroidType type)
    {
        Vector3 position = _instance._spawnCoord[Random.Range(0,_instance._spawnCoord.Count - 1)].position;
        Asteroid asteroid = factory.Get(type);
        asteroid.SpawnAsteroid(position, _instance._vectorAsteroidMove, position);
       _instance._asteroid.Add(asteroid); 
    }

    public static void SpawnAsteroidAfterDieBigAsteroid(Vector3 position, AsteroidType type, Vector3 startPosition)
    {
        Asteroid asteroid = _instance._asteroidFactory.Get(type);
        Asteroid asteroid2 = _instance._asteroidFactory.Get(type);

        asteroid.SpawnAsteroid(position, _instance._vectorAsteroidMove, startPosition);
        asteroid2.SpawnAsteroid(position, _instance._vectorAsteroidMove, startPosition);

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
