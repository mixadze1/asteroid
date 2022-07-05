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

        Vector3 movementVertical = new Vector3(vertical * transform.forward.x, 0, vertical * transform.forward.z);
        Vector3 movementHorizontal = new Vector3(horizontal * transform.right.x, 0, horizontal * transform.right.z);

        if (movementVertical.magnitude > 0 )
        {
            transform.Translate(MovementCalculate(movementVertical), Space.World);
        }

        if (movementHorizontal.magnitude > 0)
        {
            transform.Translate(MovementCalculate(movementHorizontal), Space.World);
        }
        AnimationMove(movementVertical, movementHorizontal);
    }
 
    private Vector3 MovementCalculate(Vector3 movement)
    {
        movement.Normalize();
        movement *= _speed * Time.deltaTime;

        return movement;
    }

    private void AnimationMove(Vector3 movementVertical, Vector3 movementHorizontal)
    {
        float velocityZ = Vector3.Dot(movementVertical.normalized, transform.forward);
        float velocityX = Vector3.Dot(movementHorizontal.normalized, transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}