using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffects : MonoBehaviour
{
    [SerializeField] ParticleSystem _bossTeleportEffect = null;

    public void OnTeleport()
    {
        if (_bossTeleportEffect != null)
        {
            ParticleSystem effect = Instantiate(_bossTeleportEffect, this.transform, false);
            Destroy(effect.gameObject, 5f);
        }

    }
}
