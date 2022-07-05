using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private Rigidbody[] _rigidbody;
    [SerializeField] private float _timeToActivateRagdoll = 0.35f;
    public bool IsDie { get; private set; }

    private void Start()
    {
       _rigidbody = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in _rigidbody)
        {
           rb.isKinematic = true;
        }
    }
        
    public void TakeDamage(float damage)
    {
        _health -= damage;
    }


    public void Finishing()
    {
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(_timeToActivateRagdoll);
        foreach (Rigidbody rb in _rigidbody)
        {
            rb.isKinematic = false;
        }
        IsDie = true;
        gameObject.GetComponent<Animator>().enabled = false;

        yield return new WaitForSeconds(_timeToDestroy - _timeToActivateRagdoll);
        var model = gameObject.GetComponentInParent<Rigidbody>();
        Destroy(model.gameObject);

    }
}
