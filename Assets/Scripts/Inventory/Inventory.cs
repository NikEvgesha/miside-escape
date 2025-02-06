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

    public void RemoveItem(ItemData item)
    {
        _items.Remove(item);
        _inventoryUI.RemoveItem(item);
    }

}
