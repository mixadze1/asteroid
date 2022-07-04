using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;

    private bool _isFinishing;

    public float Movement;
    public bool IsFinishing { get => _isFinishing; set => _isFinishing = value; }
    public Animator Animator => _animator;

    public void Start()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        foreach (BoxCollider collider in GetComponentsInChildren<BoxCollider>())
        {
            collider.isTrigger = true;  
        }    
    }

    void Update()
    {
        if (_isFinishing)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        Movement = movement.magnitude;

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= _speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        
        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}