using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _movementSpeed = 3.0f;

    [Header("Outside Information")]
    [SerializeField] Transform _player;

    Rigidbody _rb;

    Vector3 _floatingPosition;
    Vector3 _movingPosition;

    bool _moving = false;

    float _offset = 10f;
    float _tolerance = 0.05f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _movingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.LookAt(_player.transform);
        _rb.freezeRotation = true;

        if (Vector3.Distance(transform.position, _player.position) > _offset + _tolerance)
        {
            MovePlayer(true);
        }
        else if (Vector3.Distance(transform.position, _player.position) < _offset - _tolerance)
        {
            MovePlayer(false);
        }
        else
        {
            transform.position = transform.position;
        }
    }

    void MovePlayer(bool direction)
    {
        if (direction)
        {
            _movingPosition += transform.forward * _movementSpeed * Time.deltaTime;
        }
        else
        {
            _movingPosition -= transform.forward * _movementSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(_movingPosition.x, transform.position.y, _movingPosition.z);
    }
}
