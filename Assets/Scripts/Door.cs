using System;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class Door : MonoBehaviour
{
    /*    [SerializeField] private Animator _doorAnimator;
        [SerializeField] private AnimationClip _doorAnimationClip;*/
    [SerializeField] private List<Animator> _doorAnimators;
    [SerializeField] private TriggerEnterChecker entryChecker;
    [SerializeField] private InteractionHint _hint;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _openTime;
    [SerializeField] private DoorUI _doorUI;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _open;

    [SerializeField] private ItemData _requiredKeyData;
    [SerializeField] private bool _isFinalDoor;

    public Action DoorOpen;
    //[SerializeField] private KeyItemType _requiredKey;

    private bool _playerInTrigger;

    private void OnEnable()
    {
        entryChecker.OnTrigger += OnPlayerEnter;
        _timer.TimerFinish += OnOpenTimerFinish;
        _timer.TimerStop += OnSearchTimerStop;
    }

    private void OnDisable()
    {
        entryChecker.OnTrigger -= OnPlayerEnter;
        _timer.TimerFinish -= OnOpenTimerFinish;
        _timer.TimerStop -= OnSearchTimerStop;
        InputListener.Instance.InteractionKeyPressed -= OnInteractionKeyPressed;
        GameManager.Instance.Reset -= ResetDoor;
    }

    private void Start()
    {
        _doorUI.SetKeyImage(_requiredKeyData.Img);
        InputListener.Instance.InteractionKeyPressed += OnInteractionKeyPressed;
        GameManager.Instance.Reset += ResetDoor;
    }


    private void ResetDoor() {
        //_doorUI.ToggleImage(true);
        _timer.gameObject.SetActive(false);
        entryChecker.gameObject.SetActive(false);
/*        foreach (var animator in _doorAnimators)
        {
            animator.SetTrigger("Reset");
            animator.ResetTrigger("Open");
        }*/
            
    }


    private void OnInteractionKeyPressed() {
        if (!_playerInTrigger) return;
        if (CheckKey())
            StartOpen();

    }

    private void OnPlayerEnter(bool inTrigger)
    {
        _playerInTrigger = inTrigger;
        if (CheckKey() || !inTrigger)
        {
            _hint.EnableInteractionAreaHint(inTrigger);
            _hint.EnableInteractionKeyHint(inTrigger);
            TouchUI.Instance.ToggleInterationButton(inTrigger);
        } else if (inTrigger)
        {
            _doorUI.ShowRequiredItemHint();
        }     
    }

    private bool CheckKey() {
        return Inventory.Instance.Contains(_requiredKeyData);
    }


    private void StartOpen()
    {
        _audioSource.Play();
        _hint.EnableInteractionKeyHint(false);
        _hint.EnableInteractionAreaHint(true);
        _timer.StartTimer(_openTime, entryChecker);
    }


    private void OnOpenTimerFinish()
    {
        if (GameManager.Instance.GameInProgress)
        {
            _audioSource.Stop();
            Inventory.Instance.RemoveItem(_requiredKeyData);
            _hint.EnableInteractionKeyHint(false);
            _doorUI.ToggleImage(false);
            entryChecker.gameObject.SetActive(false);
            _playerInTrigger = false;
            foreach (var animator in _doorAnimators)
            {
                PlayOpenSound();
                animator.SetTrigger("Open");
                animator.ResetTrigger("Reset");
                YandexMetrica.Send("DoorOpen");
            }
            TouchUI.Instance.ToggleInterationButton(false);
            if (_isFinalDoor)
            {
                GameManager.Instance.OnGameWin();
            }
            DoorOpen?.Invoke();
        }

    }
    private void OnSearchTimerStop()
    {
        _audioSource.Stop();
    }
    private void PlayOpenSound()
    {
        _audioSource.loop = false;
        _audioSource.clip = _open;
        _audioSource.Play();
    }

}
