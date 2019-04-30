using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform _target;
	public float _rotation = 30.0f;
	public Vector3 _padding = new Vector3(0.0f, 0.0f, 6.0f);

	public Vector2 _defaultRotation = new Vector2(0.0f, 30.0f);
	public float _defaultDistance = -7.0f;
	public float _rotateSpeed = 1;
	public bool _isXAxisInverted = false;
	public bool _isYAxisInverted = false;

	private LayerMask _raycastLayerMask;
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

		float horizontalAxis = Input.GetAxis("Mouse X") * _rotateSpeed * (_isXAxisInverted ? 1.0f : -1.0f);
		float verticalAxis = Input.GetAxis("Mouse Y") * _rotateSpeed * (_isYAxisInverted ? 1.0f : -1.0f);
		float scrollAxis = Input.GetAxis("Mouse ScrollWheel") * _rotateSpeed;

		_currentRotation.x = Mathf.Clamp(_currentRotation.x + verticalAxis, -10.0f, 80.0f);
		_currentRotation.y -= horizontalAxis;
		_currentDistance += scrollAxis;


		Vector3 lookDirection = Quaternion.Euler(_currentRotation.x, _currentRotation.y, 0.0f) * Vector3.forward;

		//RaycastHit raycastHit;
		float cameraDistance = _padding.z;
		//if (Physics.Raycast(_target.transform.position, -lookDirection, out raycastHit, Mathf.Infinity, _raycastLayerMask)) {
		//	cameraDistance = Mathf.Min(cameraDistance, raycastHit.distance);
		//}

		transform.position = _target.position - lookDirection * cameraDistance;
		transform.LookAt(_target);
	}

	private void ResetCamera()
	{
		_currentRotation.Set(0.0f, _rotation);
		_currentDistance = _padding.z;
	}
}
