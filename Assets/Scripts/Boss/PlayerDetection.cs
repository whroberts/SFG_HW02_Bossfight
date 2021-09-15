using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerDetection : MonoBehaviour
{
    BossController _bc;
    private void Awake()
    {
        _bc = GetComponentInParent<BossController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _bc.EventTest();
        }
    }
}
