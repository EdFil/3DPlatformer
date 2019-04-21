using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float _moveSpeed = 1.0f;
	public float _jumpForce = 1.0f;
	public float _gravityScale = 1.0f;

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

		// Update player motion
		_playerMotion.Set(horizontalAxis * _moveSpeed, _playerMotion.y, verticalAxis * _moveSpeed);

		if (_characterController.isGrounded)
		{
			if (isJumpPressed)
				_playerMotion.y = _jumpForce;
			else
				_playerMotion.y = Physics.gravity.y * _gravityScale * Time.deltaTime;
		} else
		{
			_playerMotion.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
		}

		_characterController.Move(_playerMotion * Time.deltaTime);
	}
}
