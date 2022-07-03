using System.Collections;
using UnityEngine;

public class Enemy : GameBehavior
{
    [SerializeField] private Transform _spawnShellCoord;
    [SerializeField] private Transform _explosion;

    private Transform _model;
    private SpaceShip _target;

    private Vector3 _startPosition;

    private float _speedShell;
    private float _damage;
    private float _timeToShoot;
    private float _timeLiveShell;
    private float _speed;
    private float _health;

    private float offset = 3.5f; 

    public EnemyFactory OriginFactory { get; set; }

    public void Initialize(float scale, float timeToShoot, float speed, float health, float timeLiveShell, 
        float damage, float speedShell)
    {
        _speedShell = speedShell;
        _damage = damage;
        _timeToShoot = timeToShoot;
        _timeLiveShell = timeLiveShell;
        _model = this.transform;
        _model.localScale = new Vector3(scale, scale, scale);
        _speed = speed;
        _health = health;
    }

    public void SpawnOn(Transform transform, SpaceShip spaceShip)
    {
        _model.localPosition = transform.localPosition;
        _startPosition = transform.position ;
        _target = spaceShip;
        StartCoroutine(Shoot());
    }

    public void Update()
    {
        if(_health <= 0)
        {
            SfxAudio._instance.DieNlo.Play();
            Instantiate(_explosion,transform.position, Quaternion.identity);
            GUIManager._instance.Score += GUIManager._instance.NloScore;
            Recycle();
        }
        if (_startPosition.x < 0)
            MoveNlo(_speed);
        else
            MoveNlo(- _speed);
    }

    private void MoveNlo(float speed)
    {
        transform.position = new Vector3(transform.localPosition.x + speed, Mathf.Sin(Time.fixedTime) + offset, transform.localPosition.z);
    }

    private IEnumerator Shoot()
    {   while(true)
        {
            yield return new WaitForSeconds(_timeToShoot);
            Game.SpawnShell(false)?.Initialize(_spawnShellCoord, _speedShell, _damage, false, _timeLiveShell, _target);
            SfxAudio._instance.ShootNlo.Play();
        }
       
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public override void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}
