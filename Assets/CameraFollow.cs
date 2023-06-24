using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.5f;
    public float offsetDistance = 10.0f;
    public float currentYAngle = 30.0f;
    public Vector3 horizontalDirection = new Vector3(0, 0, -1);
    public float maxYAngle = 45.0f;
    public float minYAngle = 10.0f;
    public float ySensitivity = 0.3f;
    public float xSensitivity = 3.0f;
    private float _yInput;
    private float _xInput;
    private Vector3 _offset;
    private Vector3 _velocity = Vector3.zero;

    public void OnLookY(InputAction.CallbackContext context)
    {
        _yInput = context.ReadValue<float>();
    }
    public void OnLookX(InputAction.CallbackContext context)
    {
        _xInput = context.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateAngle();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            UpdateAngle();
            Vector3 _targetPosition = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, smoothTime);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-_offset), smoothTime);
        }

    }

    void UpdateAngle()
    {
        currentYAngle += _yInput * ySensitivity;
        currentYAngle = Mathf.Clamp(currentYAngle, minYAngle, maxYAngle);

        horizontalDirection = Quaternion.Euler(0, _xInput * xSensitivity, 0) * horizontalDirection;

        float _yOffset = Mathf.Sin(currentYAngle * Mathf.PI / 180) * offsetDistance;
        float _offsetHorizontal = Mathf.Cos(currentYAngle * Mathf.PI / 180) * offsetDistance;

        float _xOffset = horizontalDirection.x * _offsetHorizontal;
        float _zOffset = horizontalDirection.z * _offsetHorizontal;

        _offset = new Vector3(_xOffset, _yOffset, _zOffset);
    }
}
