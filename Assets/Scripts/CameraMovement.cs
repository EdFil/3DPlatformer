using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform _target;
	public Vector2 _defaultRotation = new Vector2(0.0f, 30.0f);
	public float _defaultDistance = -7.0f;
	public float _rotateSpeed = 1;
	public LayerMask _raycastLayerMask;

	private Vector2 _currentRotation;
	private float _currentDistance;

	// Start is called before the first frame update
	void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		ResetCamera();

		_raycastLayerMask = ~(1 << LayerMask.NameToLayer("Player"));
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

		Vector3 lookDirection = Vector3.ProjectOnPlane(_target.forward, Vector3.up);
		Quaternion xRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
		Quaternion yRotation = Quaternion.AngleAxis(_currentRotation.y, Vector3.right);
		lookDirection = xRotation * yRotation * lookDirection;

		RaycastHit raycastHit;
		float cameraDistance = _currentDistance;
		if (Physics.Raycast(_target.transform.position, -lookDirection, out raycastHit, Mathf.Infinity, _raycastLayerMask)) {
			cameraDistance = Mathf.Min(cameraDistance, raycastHit.distance);
		}

		transform.position = _target.position - lookDirection * cameraDistance;
		transform.LookAt(_target);
	}

	private void ResetCamera()
	{
		_currentRotation = _defaultRotation;
		_currentDistance = _defaultDistance;
	}
}
