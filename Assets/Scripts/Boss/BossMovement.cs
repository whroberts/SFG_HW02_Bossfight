using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _movementSpeed = 3.0f;

    [Header("Components")]
    [SerializeField] GameObject _bossArt;

    [Header("Outside Information")]
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _playableAreaMin;
    [SerializeField] Vector3 _playableAreaMax;

    Rigidbody _rb;

    Vector3 _movingPosition;

    Vector3 teleportStartingLocation;
    Vector3 teleportLandingLocation;

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

    
    public IEnumerator Teleport()
    {
        teleportStartingLocation = transform.position;

        teleportLandingLocation = new Vector3(Random.Range(_playableAreaMin.x, _playableAreaMax.x),
            Random.Range(_playableAreaMin.y, _playableAreaMax.y), Random.Range(_playableAreaMin.z, _playableAreaMax.z));
        _bossArt.SetActive(false);

        Debug.Log("Starting: " + teleportStartingLocation);
        Debug.Log("Landing: " + teleportLandingLocation);

        yield return new WaitForSeconds(1f);

        transform.position = teleportLandingLocation;
        _movingPosition = transform.position;
        _bossArt.SetActive(true);
    }
}
