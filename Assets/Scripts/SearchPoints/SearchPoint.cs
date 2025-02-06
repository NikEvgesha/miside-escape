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
    private KeyCode _interactionKey = KeyCode.E;


    public List<KeyItemType> GetAvailableKeyTypes() 
    {
        return _availableKeyTypes;
    }

    public void SetItem(ItemData data) {
        _item = data;
    }


    private void OnEnable()
    {
        entryChecker.OnTrigger += OnPlayerEnter;
        _timer.TimerFinish += OnSearchTimerFinish;
    }

    private void OnDisable()
    {
        entryChecker.OnTrigger -= OnPlayerEnter;
        _timer.TimerFinish -= OnSearchTimerFinish;
    }


    private void Update()
    {
        if (_playerInTrigger && SimpleInput.GetKeyDown(_interactionKey)) {
            StartSearch();
        }
    }

    private void OnPlayerEnter(bool inTrigger)
    {
        _playerInTrigger = inTrigger;
        _hint.EnableInteractionHint(inTrigger);
    }


    private void StartSearch() {
        _hint.EnableInteractionHint(false);
        _timer.StartTimer(_searchTime, entryChecker);
    }


    private void OnSearchTimerFinish() {
        entryChecker.gameObject.SetActive(false);

        if (_item != null)
        {
            Inventory.Instance.AddItem(_item);
        }
        else {
            _hint.ShowEmptyHint();
        }
        //Destroy(gameObject);
    }

}
