using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    [SerializeField] Vector3 _playableAreaMin = Vector3.zero;
    [SerializeField] Vector3 _playableAreaMax = Vector3.zero;
    [SerializeField] GameObject _teleportCheckObject = null;

    bool _isTeleporting = false;
    public bool IsTeleporting => _isTeleporting;

    MeshRenderer[] _bossVisuals = null;
    Collider _collider = null;
    Collider _teleportCheckCollider = null;

    BossEffects _bossEffects = null;
    BossMovement _bossMovement = null;
    BossController _bossController = null;

    Vector3 _teleportStartingLocation;
    Vector3 _teleportLandingLocation;
    Vector3 _checkStartLocation;

    private void Awake()
    {
        _bossVisuals = GetComponentsInChildren<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _teleportCheckCollider = _teleportCheckObject.GetComponent<Collider>();

        _bossEffects = GetComponent<BossEffects>();
        _bossMovement = GetComponent<BossMovement>();
        _bossController = GetComponent<BossController>();

        _checkStartLocation = _teleportCheckObject.transform.position;
    }

    public IEnumerator Teleport()
    {
        _isTeleporting = true;
        _collider.enabled = false;
        _teleportStartingLocation = transform.position;

        _teleportLandingLocation = new Vector3(Random.Range(_playableAreaMin.x, _playableAreaMax.x),
           Random.Range(_playableAreaMin.y, _playableAreaMax.y), Random.Range(_playableAreaMin.z, _playableAreaMax.z));

        _teleportCheckCollider.enabled = true;
        _teleportCheckObject.transform.position = _teleportLandingLocation;

        yield return new WaitForSeconds(0.05f);

        StartCoroutine(BossVisuals(false));

        yield return new WaitForSeconds(1.5f);

        _isTeleporting = false;
        transform.position = _teleportLandingLocation;
        _bossMovement._movingPosition = transform.position;

        _teleportCheckCollider.enabled = false;
        _collider.enabled = true;
        StartCoroutine(BossVisuals(true));
    }

    private IEnumerator BossVisuals(bool state)
    {
        _bossEffects.OnTeleport();

        yield return new WaitForSeconds(0.5f);

        //_collider.enabled = state;
        for (int i = 0; i < _bossVisuals.Length; i++)
        {
            _bossVisuals[i].enabled = state;
        }

        if (state)
        {
            _teleportCheckObject.transform.localPosition = new Vector3(0, 2, 0);
            _bossController._attacking = false;
        }
    }
}
