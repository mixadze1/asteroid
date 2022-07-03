using UnityEngine;

public class TargetPointCheckSpaceShip : MonoBehaviour
{
    private float _damageEnemy = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpaceShip>())
        {
            collision.GetComponent<SpaceShip>()?.TakeDamage(_damageEnemy);

            if (gameObject.GetComponent<Shell>())
                Destroy(gameObject);
        }
    }
}

