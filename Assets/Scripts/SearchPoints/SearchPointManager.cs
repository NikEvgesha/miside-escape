using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchPointManager : MonoBehaviour
{

    [SerializeField] private List<ItemData> _keyDatas;

    private List<SearchPoint> _searchPoints;
    private Dictionary<KeyItemType, List<SearchPoint>> _availablePlaces;

    private HashSet<SearchPoint> _selectedPlaces;
    private void Start()
    {
        GameManager.Instance.Reset += OnGameRestart;
        _searchPoints = GetComponentsInChildren<SearchPoint>().ToList();
        _availablePlaces = new Dictionary<KeyItemType, List<SearchPoint>>();
        _selectedPlaces = new HashSet<SearchPoint>();

        CollectDataFromPoints();
        PutItems();
        
    }

    private void OnDisable()
    {
        GameManager.Instance.Reset -= OnGameRestart;
    }


    private void CollectDataFromPoints()
    {
        foreach (var keyData in _keyDatas)
        {
            _availablePlaces.Add(keyData.Type, new List<SearchPoint>());
        }

        foreach (var point in _searchPoints)
        {
            List<KeyItemType> availableItems = point.GetAvailableKeyTypes();
            foreach (var keyType in availableItems)
            {
                _availablePlaces[keyType].Add(point);
            }
        }

        _keyDatas.Sort((e1, e2) => _availablePlaces[e1.Type].Count.CompareTo(_availablePlaces[e2.Type].Count));
    }

    private void PutItems() {
        foreach (var keyData in _keyDatas)
        {
            KeyItemType keyType = keyData.Type;
            bool _added = false;

            while (!_added)
            {
                int availablePlacesCount = _availablePlaces[keyType].Count;
                int randomChoice = Random.Range(0, availablePlacesCount);

                SearchPoint point = _availablePlaces[keyType][randomChoice];

                if (!_selectedPlaces.Contains(point))
                {
                    point.SetItem(keyData);
                    _selectedPlaces.Add(point);
                    _added = true;
                }
                else
                {
                    _availablePlaces[keyType].Remove(point);
                }
            }
        }
    }

    private void OnGameRestart() {
        _selectedPlaces.Clear();
        foreach (var point in _searchPoints) {
            point.ResetPoint();
        }
        PutItems();
    }
}
