using System.Collections.Generic;
using UnityEngine;

public class SearchPoint : MonoBehaviour
{

    [SerializeField] private TriggerEnterChecker entryChecker;
    [SerializeField] private InteractionHint _hint;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _searchTime; // Подумать, куда это пихнуть

    [SerializeField] private List<KeyItemType> _availableKeyTypes;



    public ItemData _item = null;
    private bool _playerInTrigger;

    /*-----public----*/

    public List<KeyItemType> GetAvailableKeyTypes() 
    {
        return _availableKeyTypes;
    }

    public void SetItem(ItemData data) {
        _item = data;
    }


    public void ResetPoint()
    {
        _item = null;
        entryChecker.gameObject.SetActive(true);
    }


    /*-----private----*/

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
        InputListener.Instance.InteractionKeyPressed += OnInteractionKeyPressed;
    }


    private void OnInteractionKeyPressed() {
        if (!_playerInTrigger) return;

        StartSearch();

    }

    private void OnPlayerEnter(bool inTrigger)
    {
        _playerInTrigger = inTrigger;
        _hint.EnableInteractionKeyHint(inTrigger);
        TouchUI.Instance.ToggleInterationButton(inTrigger);
    }


    private void StartSearch() {
        _hint.EnableInteractionKeyHint(false);
        _timer.StartTimer(_searchTime, entryChecker);
    }


    private void OnSearchTimerFinish() {
        entryChecker.gameObject.SetActive(false);
        _hint.EnableInteractionKeyHint(false);
        TouchUI.Instance.ToggleInterationButton(false);
        _playerInTrigger = false;
        if (_item != null)
        {
            Inventory.Instance.AddItem(_item);
        }
        else {
            _hint.ShowEmptyHint();
        }
        
    }


}
