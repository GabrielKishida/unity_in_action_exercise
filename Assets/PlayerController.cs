using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float baseSpeed = 5.0f;
    public float maxSpeed = 9.0f;
    public float minSpeed = 0.5f;
    public float smoothFactor = 0.2f;
    public float rememberFactor = 0.5f;
    private Vector2 _move;
    private Vector3 _lastMovement;

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 _movement = new Vector3(_move.x, 0f, _move.y);
        _movement *= baseSpeed;
        _movement += _lastMovement * rememberFactor;

        if (_movement.magnitude > 0)
        {
            _movement = Vector3.ClampMagnitude(_movement, maxSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), smoothFactor);
            transform.Translate(_movement * Time.deltaTime, Space.World);

            if (_movement.magnitude < minSpeed) { _lastMovement = Vector3.zero; }
            else { _lastMovement = _movement; }

        }

    }
}
