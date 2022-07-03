using UnityEngine;

public class TargetPointCheckEnemy : MonoBehaviour
{
    private float _damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
