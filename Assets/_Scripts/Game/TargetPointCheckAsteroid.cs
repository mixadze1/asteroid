using UnityEngine;

public partial class TargetPointCheckAsteroid : MonoBehaviour
{
    private float _damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Asteroid>())
        {
            collision.GetComponent<Asteroid>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
