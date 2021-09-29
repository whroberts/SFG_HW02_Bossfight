using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _movementSpeed = 5.0f;
    [SerializeField] float _turnSpeed = 3.0f;

    [Header("Outside Information")]
    [SerializeField] Transform _player = null;
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
        if (_player != null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _player.rotation, _turnSpeed * Time.deltaTime);
            transform.LookAt(_player.transform);

            if (gameObject.CompareTag("E"))
            {
                _rb.freezeRotation = false;
            }
            else
            {
                _rb.freezeRotation = true;
            }

            if (Vector3.Distance(_rb.position, _player.position) > _offset + _tolerance)
            {
                MoveBoss(true);
            }
            else if (Vector3.Distance(_rb.position, _player.position) < _offset - _tolerance)
            {
                if (Vector3.Distance(_rb.position, _player.position) < 8f)
                {
                    if (!_bossTeleport.IsTeleporting)
                    {
                        StartCoroutine(_bossTeleport.Teleport());
                    }
                }
                else
                {
                    MoveBoss(false);
                }
            }
            else
            {
                _rb.position = _rb.position;
            }
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

        _rb.position = new Vector3(_movingPosition.x, _rb.position.y, _movingPosition.z);
    }
}
