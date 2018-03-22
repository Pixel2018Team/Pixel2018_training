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
        Debug.DrawLine(transform.position, transform.position + transform.rotation * Vector3.forward * 3, Color.red);

        _moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveVelocity = _moveInput * speed;

        Vector3 newDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * Input.GetAxisRaw("RVertical");
        if(newDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _moveVelocity;
    }
}