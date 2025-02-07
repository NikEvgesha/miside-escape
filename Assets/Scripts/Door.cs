using UnityEngine;
using UnityEngine.UI;


public class Door : MonoBehaviour
{
    /*    [SerializeField] private Animator _doorAnimator;
        [SerializeField] private AnimationClip _doorAnimationClip;*/
    [SerializeField] private Animator _doorAnimator;
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
        GameManager.Instance.GameRestart -= ResetDoor;
    }

    private void Start()
    {
        _doorUI.SetKeyImage(_requiredKeyData.Img);
        InputListener.Instance.InteractionKeyPressed += OnInteractionKeyPressed;
        GameManager.Instance.GameRestart += ResetDoor;
    }


    private void ResetDoor() {
        _doorUI.ToggleImage(true);
        entryChecker.gameObject.SetActive(true);
        _doorAnimator.SetTrigger("Reset");
        _doorAnimator.ResetTrigger("Open");
    }


    private void OnInteractionKeyPressed() {
        if (!_playerInTrigger) return;

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
        _doorAnimator.SetTrigger("Open");
        _doorAnimator.ResetTrigger("Reset");
        TouchUI.Instance.ToggleInterationButton(false);
        if (_isFinalDoor) {
            GameManager.Instance.OnGameWin();
        }
    }

}
