using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform _target;
	public float _defaultDistance = 7.0f;
	public Vector2 _defaultRotation = new Vector2(0.0f, 20.0f);
	public float _rotateSpeed = 1;
	public bool _isXAxisInverted = false;
	public bool _isYAxisInverted = false;

	private Vector2 _currentRotation;
	private float _currentDistance;
	public LayerMask _layerMask;

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

		float horizontalAxis = Input.GetAxis("Mouse X") * _rotateSpeed * (_isXAxisInverted ? 1.0f : -1.0f);
		float verticalAxis = Input.GetAxis("Mouse Y") * _rotateSpeed * (_isYAxisInverted ? 1.0f : -1.0f);
		float scrollAxis = Input.GetAxis("Mouse ScrollWheel") * _rotateSpeed;

		_currentRotation.x = Mathf.Clamp(_currentRotation.x + verticalAxis, -10.0f, 80.0f);
		_currentRotation.y -= horizontalAxis;
		_currentDistance += scrollAxis;

		Vector3 lookDirection = Quaternion.Euler(_currentRotation.x, _currentRotation.y, 0.0f) * Vector3.forward;
		float distanceForCamera = _currentDistance;		
		
		RaycastHit hitInfo;
		if (Physics.Raycast(_target.position, -lookDirection, out hitInfo, _currentDistance, _layerMask)) {
			distanceForCamera = hitInfo.distance;
		}

		transform.position = _target.position - lookDirection * distanceForCamera;
		transform.LookAt(_target);
	}

	private void ResetCamera()
	{
		_currentRotation.Set(_defaultRotation.y, Vector3.Angle(_target.forward, Vector3.forward));
		_currentDistance = _defaultDistance;
	}
}
