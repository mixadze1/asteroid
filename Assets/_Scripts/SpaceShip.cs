using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : GameBehavior
{
    [SerializeField] private Transform _model;
    [SerializeField] private Transform _spawnShellCoord;
    [SerializeField, Range(0.05f, 0.4f)] private float _speedShell = 0.1f;
    private float _speedMax;
    private float _speed;
    private float _speedRotate;
    private float _pathOffset;
    private float _inertia;
    private float _speedDamping;
    private float _scale;
    private float _health;
    private float _timeImmortal = 3f;
    private float _currentSpeed;
    private float _timeBlinking = 0.4f;
    private GameControl _gameControl;
    private Vector3 _rememberVector;

    private bool _isImmortal;
    private bool _isStartGame = true;


    public float Damage { get; private set; }


    public SpaceShipFactory OriginFactory { get; set; }
    public void Initialize(float rotate, float scale, float speed, float inertia, float health, float damage, float speedDamping)
    {
        _speedRotate = rotate;
        _model.localScale = new Vector3(scale, scale, scale);
        _speed = speed;
        _inertia = inertia;
        _health = health;
        _scale = scale;
        Damage = damage;
        _speedDamping = speedDamping;
        GUIManager._instance.Health = (int)_health;
    }

    public void SpawnOn(GameControl gameControl)
    {
        _gameControl = gameControl;
        _model.localPosition = Vector3.zero;
        StartCoroutine(StartImmortal());
    }

    public void Update()
    {
        if (_health <= 0)
            Destroy(this.gameObject);

        if (_gameControl == GameControl.KeyBoard)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        if (_gameControl == GameControl.Mouse)
        {
            float mouse = Input.GetAxis("Mouse X");
            if (mouse < 0)
            {
                RotateShip(_speedRotate / 3);
            }
            if (mouse > 0)
            {
                RotateShip(-_speedRotate / 3);
            }
        }
    }

    public void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            AccelerationShip();
        }

        else if (_isStartGame)
        {
            return;
        }
        else if (!_isStartGame)
        {
            MoveInertia();
        }

        if (_gameControl == GameControl.KeyBoard)
        {
            if (Input.GetKey(KeyCode.A))
            {
                RotateShip(_speedRotate);
            }

            if (Input.GetKey(KeyCode.D))
            {
                RotateShip(-_speedRotate);
            }
        }

    }

    private IEnumerator StartImmortal()
    {
        float count = 0;
        for (count = 0; count <= _timeImmortal;)
        {
            yield return new WaitForSeconds(_timeBlinking);
            count += _timeBlinking;
            _model.gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(_timeBlinking);
            count += _timeBlinking;
            _model.gameObject.GetComponent<Renderer>().enabled = true;
        }

        _isImmortal = false;

    }


    public void TakeDamage(float damage)
    {
        if (_isImmortal)
            return;

        _health -= damage;
        GUIManager._instance.Health = (int)_health;
    }

    private void Shoot()
    {
        Game.SpawnShell(true).Initialize(_spawnShellCoord, _speedShell, Damage, Color.red,true);
    }

    private void MoveInertia()
    {
        transform.Translate(_rememberVector * _inertia, Space.World);
    }

    private void AccelerationShip()
    {
        _isStartGame = false;
        transform.Translate(transform.up * _speed, Space.World);
        _rememberVector = transform.up;
    }

    private void RotateShip(float sideRotate)
    {
        this.transform.localRotation *= Quaternion.Euler(0f, 0f, sideRotate);
    }

    public override void Recycle()
    {
        if (gameObject != null)
            OriginFactory.Reclaim(this);
    }
}

