using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointCheckNlo : MonoBehaviour
{
    private float _damageEnemy = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>()?.TakeDamage(_damageEnemy);

            if (gameObject.GetComponent<Shell>())
                Destroy(gameObject);
        }
    }
}
