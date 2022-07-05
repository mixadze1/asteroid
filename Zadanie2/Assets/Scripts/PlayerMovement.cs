using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _speed = 5f;

    private bool _isFinishing;
    private bool _isCrashedInEnemy;

    public float Movement {  get; private set; }
    public bool IsFinishing { get => _isFinishing; set => _isFinishing = value; }
    public bool IsCrashedInEnemy { get => _isCrashedInEnemy; set => _isCrashedInEnemy = value; }
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
        _rb.isKinematic = false;
        _collider.isTrigger = false;
    }

    void Update()
    {
        if (_isFinishing)
        {
            _rb.velocity = Vector3.zero;
            return;
        }   

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementVertical = new Vector3(vertical * transform.forward.x, 0f, vertical * transform.forward.z);
        Vector3 movementHorizontal = new Vector3(horizontal * transform.right.x, 0f, horizontal * transform.right.z);

        if (movementVertical.magnitude > 0 || movementHorizontal.magnitude > 0)
        {
            _rb.velocity = (MovementCalculate(movementVertical + movementHorizontal));
        }

        AnimationMove(movementVertical, movementHorizontal);
    }
 
    private Vector3 MovementCalculate(Vector3 movement)
    {
        movement *= _speed;
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