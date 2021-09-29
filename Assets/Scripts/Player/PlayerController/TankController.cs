using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 0.25f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    [SerializeField] float _moveSpeed = 0.25f;
    [SerializeField] float _turnSpeed = 2f;

    Rigidbody _rb = null;

    Vector3 _startPosition;
    float _flipLimit = 0.7f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = _rb.position;
    }

    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
        ResetPositionCheck();
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _moveSpeed * MaxSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void ResetPositionCheck()
    {
        

        if (-_flipLimit > _rb.rotation.x || _rb.rotation.x > _flipLimit || -_flipLimit > _rb.rotation.z || _rb.rotation.z > _flipLimit)
        {
            _rb.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
