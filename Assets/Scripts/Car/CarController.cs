using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[OrderAfter(typeof(ChildSync))]
public class CarController : NetworkBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform ball;
    [SerializeField] private float gravity;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float steering;

    [Networked] public Quaternion Rotation {get; set;}
    [Networked] public TickTimer Dead { get; set; }

    private Rigidbody _ballRB;
    private Transform _transform;
    private float _speed;
    private float _rotate;
    private float _currentSpeed;
    private float _currentRotate;

    private void Awake() {
        _transform = transform;
        _ballRB = ball.GetComponent<Rigidbody>();
        NetworkTransform test;
    }

    public override void Spawned() {

    }

    public override void FixedUpdateNetwork()
    {
        //model.position = ball.position;
        _transform.position = ball.position;
        ball.localPosition = new Vector3();

        PlayerInput input;
        if (!GetInput(out input))
            return;

        if (_transform.position.y < -20)
            GetComponent<IAttackable>().Damage(1000f);
        
        // (Extra) Gravity
        _ballRB.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // Check if able to move
        if (!Dead.ExpiredOrNotRunning(Runner))
            return;
        
        // Speed variable
        if (input.Forward && !input.Backward) {
            _speed = maxSpeed;
        } else if (input.Backward && !input.Forward) {
            _speed = -maxSpeed;
        } else {
            _speed = 0;
        }

        // Rotation variable
        if (input.Left && !input.Right) {
            _rotate = -steering;
        } else if (input.Right && !input.Left) {
            _rotate = steering;
        } else {
            _rotate = 0;
        }

        // Current speed & rotation
        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _speed, 12.0f / 60.0f);
        _currentRotate = Mathf.Lerp(_currentRotate, _rotate, 4.0f / 60.0f);

        // Forward acceleration
        _ballRB.AddForce(model.forward * _currentSpeed, ForceMode.Acceleration);

        //Steering
        if (_currentSpeed != 0)
            _transform.eulerAngles = Vector3.Lerp(_transform.eulerAngles, new Vector3(0, _transform.eulerAngles.y + _currentRotate * Mathf.Abs(_currentSpeed) / maxSpeed, 0), Time.deltaTime * 5f);
    }
}
