using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Transform _pivot;
	public float _moveSpeed = 1.0f;
	public float _jumpForce = 1.0f;
	public float _gravityScale = 1.0f;

	private Vector3 _currentMoveDirection;
	private CharacterController _characterController;
	private Vector3 _playerMotion = Vector3.zero;

	// Start is called before the first frame update
	void Start()
    {
		_characterController = GetComponent<CharacterController>();
	}


	// Update is called once per frame
	void Update()
    {
		// Update Input
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		float verticalAxis = Input.GetAxisRaw("Vertical");
		bool isJumpPressed = Input.GetAxisRaw("Jump") > 0.0f;

		Vector3 movementDirection = GetMovementDirection(horizontalAxis, verticalAxis);
		Debug.DrawLine(transform.position, transform.position + movementDirection * 5.0f, Color.black);

		// Update player motion
		_playerMotion.Set(movementDirection.x * _moveSpeed, _playerMotion.y, movementDirection.z * _moveSpeed);

		if (_characterController.isGrounded)
		{
			if (isJumpPressed)
				_playerMotion.y = _jumpForce;
			else
				_playerMotion.y = Physics.gravity.y * _gravityScale * Time.deltaTime;
		}
		else
		{
			_playerMotion.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
		}

		_characterController.Move(_playerMotion * Time.deltaTime);
	}

	private Vector3 GetMovementDirection(float horizontalAxis, float verticalAxis)
	{
		if (horizontalAxis == 0.0f && verticalAxis == 0.0f)
			return Vector3.zero;

		// Calculate rotation angle based on Axis input
		float angle = 0.0f;
		if (horizontalAxis != 0.0f && verticalAxis != 0.0f) {
			angle = 45.0f * horizontalAxis + (verticalAxis < 0.0f ? 90.0f * horizontalAxis : 0.0f);
		} else {
			angle = 90.0f * horizontalAxis + (verticalAxis < 0.0f ? 180.0f : 0.0f);
		}

		return (Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.ProjectOnPlane(_pivot.forward, Vector3.up)).normalized;
	}
}
