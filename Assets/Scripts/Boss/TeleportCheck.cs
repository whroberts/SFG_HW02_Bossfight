using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCheck : MonoBehaviour
{
    BossTeleport _bossTeleport;

    Vector3 _startLocation;

    private void Awake()
    {
        _bossTeleport = GetComponentInParent<BossTeleport>();

        _startLocation = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            StopCoroutine(_bossTeleport.Teleport());
            StartCoroutine(_bossTeleport.Teleport());
        }
    }
}
