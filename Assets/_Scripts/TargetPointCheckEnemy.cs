using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointCheckEnemy : MonoBehaviour
{
    private float _damage;
    private float _damageEnemy = 1;

    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().TakeDamage(_damage);
            gameObject.SetActive(false);
        }

        if (collision.GetComponent<Asteroid>())
        {
            collision.GetComponent<Asteroid>().TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}