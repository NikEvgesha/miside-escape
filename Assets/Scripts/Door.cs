using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField] private ItemData _requiredKeyData;
    [SerializeField] private bool _isFinalDoor;
    //[SerializeField] private KeyItemType _requiredKey;

    private bool _playerInTrigger;

    private void OnEnable()
    {
        entryChecker.OnTrigger += OnPlayerEnter;
        _timer.TimerFinish += OnOpenTimerFinish;
    }

    private void OnDisable()
    {
        entryChecker.OnTrigger -= OnPlayerEnter;
        _timer.TimerFinish -= OnOpenTimerFinish;
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
        _doorUI.ToggleImage(true);
        entryChecker.gameObject.SetActive(true);
        foreach (var animator in _doorAnimators)
        {
            animator.SetTrigger("Reset");
            animator.ResetTrigger("Open");
        }
            
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
            _hint.EnableInteractionHint(inTrigger);
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
        Inventory.Instance.RemoveItem(_requiredKeyData);
        _hint.EnableInteractionHint(false);
        _timer.StartTimer(_openTime, entryChecker);
    }


    private void OnOpenTimerFinish()
    {
        _doorUI.ToggleImage(false);
        entryChecker.gameObject.SetActive(false);
        _playerInTrigger = false;
        foreach (var animator in _doorAnimators)
        {
            animator.SetTrigger("Open");
            animator.ResetTrigger("Reset");
        }
        TouchUI.Instance.ToggleInterationButton(false);
        if (_isFinalDoor) {
            GameManager.Instance.OnGameWin();
        }
    }

}
