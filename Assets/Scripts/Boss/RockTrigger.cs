using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    Rock _rock;

    private void Awake()
    {
        _rock = GetComponentInParent<Rock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _rock.ExtraTrigger(other);
    }
}
