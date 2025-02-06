using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactionButton;
    private static TouchUI _instance;
    public static TouchUI Instance {
        get { return _instance; }
        private set { _instance = value; }
    }



    private void Awake()
    {
        _instance = this;
    }



    public void ToggleInterationButton(bool active) {
        _interactionButton.SetActive(active);
    }
}
