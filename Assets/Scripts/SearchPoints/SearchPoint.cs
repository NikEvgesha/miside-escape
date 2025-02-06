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
        InputListener.Instance.InteractionKeyPressed -= OnInteractionKeyPressed;
    }


    /*    private void Update()
        {
            if (_playerInTrigger) {
                if (InputListener.Instance.InteractionKeyPressed)
                {
                    Debug.Log("E pressed in trigger area");
                    StartSearch();
                }

            }
        }*/

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
        _hint.EnableInteractionHint(inTrigger);
        TouchUI.Instance.ToggleInterationButton(inTrigger);
    }


    private void StartSearch() {
        Debug.Log("Search");
        _hint.EnableInteractionHint(false);
        _timer.StartTimer(_searchTime, entryChecker);
    }


    private void OnSearchTimerFinish() {
        entryChecker.gameObject.SetActive(false);
        _playerInTrigger = false;
        if (_item != null)
        {
            Inventory.Instance.AddItem(_item);
        }
        else {
            _hint.ShowEmptyHint();
        }
        TouchUI.Instance.ToggleInterationButton(false);
        //Destroy(gameObject);
    }

}
