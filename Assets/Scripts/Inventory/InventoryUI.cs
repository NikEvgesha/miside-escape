using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemUI _prefab;


    private Dictionary<ItemData, ItemUI> _items;


    private void Start()
    {
        _items = new Dictionary<ItemData, ItemUI>();
    }

    public void AddItem(ItemData data) {
        ItemUI newItem = Instantiate(_prefab, transform);
        _items.Add(data, newItem);
        newItem.Init(data);
    }

    public void RemoveItem(ItemData data) {
        if (_items.ContainsKey(data)) {
            Debug.Log("UI delete item");
            Destroy(_items[data].gameObject);
            _items.Remove(data);
        }
    }

}
