using UnityEngine;
using UnityEngine.UI;


public class Door : MonoBehaviour
{
    /*    [SerializeField] private Animator _doorAnimator;
        [SerializeField] private AnimationClip _doorAnimationClip;*/
    [SerializeField] private Animation _doorAnimation;
    [SerializeField] private TriggerEnterChecker entryChecker;
    [SerializeField] private InteractionHint _hint;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _openTime;
    [SerializeField] private DoorUI _doorUI;

    [SerializeField] private ItemData _requiredKeyData;
    //[SerializeField] private KeyItemType _requiredKey;

    private bool _playerInTrigger;

    private void OnEnable()
    {
        entryChecker.OnTrigger += OnPlayerEnter;
        _timer.TimerFinish += OnSearchTimerFinish;
    }

    private void OnDisable()
    {
        entryChecker.OnTrigger -= OnPlayerEnter;
        _timer.TimerFinish -= OnSearchTimerFinish;
        InputListener.Instance.InteractionKeyPressed -= OnInteractionKeyPressed;
    }

    private void Start()
    {
        _doorUI.SetKeyImage(_requiredKeyData.Img);
        InputListener.Instance.InteractionKeyPressed += OnInteractionKeyPressed;
    }


/*    private void Update()
    {
        if (_playerInTrigger && InputListener.Instance.InteractionKeyPressed)
        {
            if (CheckKey())
            {
                StartOpen();
            }
            else 
            {
                _doorUI.ShowRequiredItemHint();
            }
        }
    }*/

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


    private void OnSearchTimerFinish()
    {
        _doorUI.ToggleImageEnable(false);
        entryChecker.gameObject.SetActive(false);
        _playerInTrigger = false;
        _doorAnimation.Play();
    }

}
