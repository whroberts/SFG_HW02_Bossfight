using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHitTextPopUp : MonoBehaviour
{
    [SerializeField] BossHealth _healthToObserve = null;
    [SerializeField] TMP_Text _textPopUpUI = null;

    [SerializeField] string _hitText = "Hit!";
    [SerializeField] string _killText = "Kill!";
    [SerializeField] float _textPopUpDuration = 1;

    BossHealth _observedHealth = null;
    Coroutine _popUpRoutine = null;

    private void Awake()
    {
        StartObservingHealth(_healthToObserve);
    }

    public void StartObservingHealth(BossHealth newHealthToObserver)
    {
        _observedHealth = newHealthToObserver;
        // notify when target is damaged
        _observedHealth.Damaged += OnObservedHealthDamaged;
        _observedHealth.Killed += OnObservedHealthKilled;
    }

    public void StopObservingHealth()
    {
        // no longer watch target
        _observedHealth.Damaged -= OnObservedHealthDamaged;
        _observedHealth.Killed -= OnObservedHealthKilled;
    }

    void OnObservedHealthDamaged(int damaged)
    {
        string hitText = _hitText + " " + damaged.ToString();

        if (_popUpRoutine != null)
        {
            StopCoroutine(_popUpRoutine);
        }
        _popUpRoutine = StartCoroutine(TextPopUp(hitText));
    }

    IEnumerator TextPopUp(string textToShow)
    {
        _textPopUpUI.text = textToShow;
        yield return new WaitForSeconds(_textPopUpDuration);
        _textPopUpUI.text = string.Empty;
    }

    void OnObservedHealthKilled()
    {
        if (_popUpRoutine != null)
        {
            StopCoroutine(_popUpRoutine);
        }
        _popUpRoutine = StartCoroutine(TextPopUp(_killText));
    }

}
