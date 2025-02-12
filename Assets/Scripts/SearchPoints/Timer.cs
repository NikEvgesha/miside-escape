using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private InteractionHint _interactionHint;

    public Action TimerFinish;
    public Action TimerStop;

    private float _currentTime;
    private float _time;
    private bool _inProgress;

    


    public void StartTimer(float time, TriggerEnterChecker checker) {
        if (!_inProgress) {
            _time = time;
            _currentTime = time;
            _interactionHint.EnableInteractionAreaHint(true);
/*            _progressBarImg.gameObject.SetActive(true);
            _progressBarImg.fillAmount = 0;*/
            _inProgress = true;
            checker.OnTrigger += StopTimer;
            StartCoroutine(Run());
        }
        
    }

    private void StopTimer(bool inTrigger) {
        if (_inProgress && !inTrigger) {
            _interactionHint.SetProgress(1);
            _inProgress = false;
            StopAllCoroutines();
            TimerStop?.Invoke();
        }
    }

    private IEnumerator Run() {
        while (_currentTime > 0) {
            _currentTime -= Time.deltaTime;
            _interactionHint.SetProgress(1f - _currentTime / _time);
            yield return null;
        }
        _inProgress = false;
        //_progressBarImg.gameObject.SetActive(false);
        TimerFinish?.Invoke();
    }



}
