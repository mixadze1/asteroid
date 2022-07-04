using UnityEngine;

public class BackStep : MonoBehaviour
{
    public bool CanBackStep { get; private set; }
    public Rigidbody[] Rb { get; private set; }
    public Enemy Enemy { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            var rb = GetComponentsInChildren<Rigidbody>();
            Enemy = other.gameObject.GetComponent<Enemy>();
            if (Enemy.IsDie)
            {
                CanBackStep = false;
            }
            else
            {
                Rb = rb;
                CanBackStep = true;
            }  
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        CanBackStep = false;
    }
}
