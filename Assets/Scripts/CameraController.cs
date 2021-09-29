using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerHealthUI _playerHealthUI = null;
    BossHealth _boss;

    Quaternion _cameraStartingRotation;

    float _length;

    private void Awake()
    {
        _cameraStartingRotation = gameObject.transform.localRotation;
        _boss = FindObjectOfType<BossHealth>();
    }

    private void FixedUpdate()
    {
        //MoveCamera();
    }

    void MoveCamera()
    {
        transform.LookAt(_boss.transform);
    }

    public IEnumerator CameraShake()
    {
        if (_playerHealthUI != null)
        {
            _length = Mathf.Pow(_playerHealthUI.EffectsStep, -1) * _playerHealthUI.EffectsLength;
            for (int i = 0; i < _length / 2; i++)
            {
                transform.rotation *= Quaternion.Euler(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f));
                yield return new WaitForSeconds(_playerHealthUI.EffectsStep);
            }
            Mathf.LerpAngle(transform.localRotation.x, _cameraStartingRotation.x, _playerHealthUI.EffectsStep * 2f * Time.deltaTime);
            Mathf.LerpAngle(transform.localRotation.y, _cameraStartingRotation.y, _playerHealthUI.EffectsStep * 2f * Time.deltaTime);
            Mathf.LerpAngle(transform.localRotation.z, _cameraStartingRotation.z, _playerHealthUI.EffectsStep * 2f * Time.deltaTime);
            transform.localRotation = _cameraStartingRotation;
        }
    }
}
