using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffects : MonoBehaviour
{
    [SerializeField] ParticleSystem _bossTeleportEffect = null;
    [SerializeField] AudioClip _teleport = null;

    public void OnTeleport()
    {
        if (_bossTeleportEffect != null)
        {
            AudioSource teleport = AudioHelper.PlayClip2D(_teleport, "Teleport: " + gameObject.name.ToString(), 0.05f, _teleport.length, 0f);
            ParticleSystem effect = Instantiate(_bossTeleportEffect, this.transform, false);

            Destroy(effect.gameObject, 5f);
            Destroy(teleport.gameObject, _teleport.length);
        }

    }
}
