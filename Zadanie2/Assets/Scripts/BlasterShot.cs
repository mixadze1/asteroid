using UnityEngine;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] private float _velocity = 15f;
    [SerializeField] private float _damage = 5;

    public void Launch(Vector3 direction)
    {
        direction.Normalize();
        transform.forward = direction;
        GetComponent<Rigidbody>().velocity = direction * _velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}