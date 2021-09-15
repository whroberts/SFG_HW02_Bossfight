using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeadMovement : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 1.0f;
    [SerializeField] bool _clockwise = true;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    void Movement(Rigidbody rb)
    {
        //impliment movement on heads
    }
}
