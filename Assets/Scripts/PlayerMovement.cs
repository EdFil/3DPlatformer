using UnityEngine;
using System.Diagnostics;

public class PlayerMovement : MonoBehaviour
{
    public Transform _pivot;
    public float _moveSpeed = 8.0f;
    public float _jumpForce = 12.0f;
    public float _gravityScale = 4.0f;
    public float _rotationSpeed;

    private CharacterController _characterController;
    private Vector3 _playerMotion = Vector3.zero;

    void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        // Update Input
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        bool isJumpPressed = Input.GetAxisRaw("Jump") > 0.0f;

        // Update direction
        Vector3 movementDirection = GetMovementDirection(horizontalAxis, verticalAxis);

        if (movementDirection != Vector3.zero) {
            transform.forward = movementDirection;
        }

        // Update motion
        _playerMotion.Set(movementDirection.x * _moveSpeed, _playerMotion.y, movementDirection.z * _moveSpeed);

        if (_characterController.isGrounded) {
            if (isJumpPressed)
                _playerMotion.y = _jumpForce;
            else
                _playerMotion.y = Physics.gravity.y * _gravityScale * _moveSpeed * Time.deltaTime;
        } else {
            _playerMotion.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
        }
        _characterController.Move(_playerMotion * Time.deltaTime);
    }

    private Vector3 GetMovementDirection(float horizontalAxis, float verticalAxis) {
        if (horizontalAxis == 0.0f && verticalAxis == 0.0f)
            return Vector3.zero;

        // Calculate rotation angle based on Axis input
        //float angle = Vector2.SignedAngle(new Vector2(0.0f, 1.0f), new Vector2(-horizontalAxis, verticalAxis));
        float angle = Mathf.Atan2(horizontalAxis, verticalAxis) * Mathf.Rad2Deg;
        UnityEngine.Debug.Log(angle);

        // Convert angle to direction based on pivot
        return Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.ProjectOnPlane(_pivot.forward, Vector3.up);
    }

}
