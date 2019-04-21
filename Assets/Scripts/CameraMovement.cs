using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform _target;
	public Vector2 _defaultRotation = new Vector2(0.0f, 30.0f);
	public float _defaultDistance = -7.0f;
	public float _rotateSpeed = 1;

	private Vector2 _currentRotation;
	private float _currentDistance;

	// Start is called before the first frame update
	void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		ResetCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R)) {
			ResetCamera();
		}

		float horizontalAxis = Input.GetAxis("Mouse X") * _rotateSpeed;
		float verticalAxis = Input.GetAxis("Mouse Y") * _rotateSpeed * -1;
		float scrollAxis = Input.GetAxis("Mouse ScrollWheel") * _rotateSpeed;

		_currentRotation.x += horizontalAxis;
		_currentRotation.y = Mathf.Clamp(_currentRotation.y + verticalAxis, 0.0f, 80.0f);
		_currentDistance += scrollAxis;

		Vector3 lookDirection = _target.forward;
		Quaternion xRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
		Quaternion yRotation = Quaternion.AngleAxis(_currentRotation.y, Vector3.right);
		lookDirection = xRotation * yRotation * lookDirection;

		transform.position = _target.position + lookDirection * _currentDistance;
		transform.LookAt(_target);
	}

	private void ResetCamera()
	{
		_currentRotation = _defaultRotation;
		_currentDistance = _defaultDistance;
	}
}
