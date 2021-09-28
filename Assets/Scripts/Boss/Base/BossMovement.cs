using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _movementSpeed = 5.0f;
    [SerializeField] float _turnSpeed = 3.0f;

    [Header("Outside Information")]
    [SerializeField] Transform _player;
    public Transform Player => _player;

    Rigidbody _rb;
    BossTeleport _bossTeleport;

    public Vector3 _movingPosition;

    float _offset = 20f;
    float _tolerance = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _bossTeleport = GetComponent<BossTeleport>();
    }

    private void FixedUpdate()
    {
        if (!_bossTeleport.IsTeleporting)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        transform.LookAt(_player.transform);
        _rb.freezeRotation = true;

        if (Vector3.Distance(transform.position, _player.position) > _offset + _tolerance)
        {
            MoveBoss(true);
        }
        else if (Vector3.Distance(transform.position, _player.position) < _offset - _tolerance)
        {
            MoveBoss(false);
        }
        else
        {
            transform.position = transform.position;
        }
    }

    private void MoveBoss(bool direction)
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
