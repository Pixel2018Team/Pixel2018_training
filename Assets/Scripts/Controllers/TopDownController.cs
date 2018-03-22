using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public float speed = 6.0F;

    private Rigidbody _rigidBody;
    private Vector3 _moveInput;
    private Vector3 _moveVelocity;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveVelocity = _moveInput * speed;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _moveVelocity;
    }
}