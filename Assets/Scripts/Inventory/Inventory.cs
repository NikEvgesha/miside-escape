using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryUI _inventoryUI;

    private List<ItemData> _items;
    private static Inventory _instance;
    public static Inventory Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _items = new List<ItemData>();
    }


    public void AddItem(ItemData item) {
        _items.Add(item);
        _inventoryUI.AddItem(item);
    }

    public bool RemoveItem(ItemData item)
    {
        bool res = _items.Remove(item);
        if (res) 
        {
            _inventoryUI.RemoveItem(item);
        }
        return res;
    }


    public bool Contains(ItemData item)
    {
        return _items.Contains(item);
    }



}
