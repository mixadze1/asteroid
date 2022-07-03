using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TargetPointCheckEnemy : MonoBehaviour
{
    private float _damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {    
            collision.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.GetComponent<Asteroid>())
        {
            collision.GetComponent<Asteroid>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
