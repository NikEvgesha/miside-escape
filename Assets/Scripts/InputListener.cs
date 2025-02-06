using System;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    private static InputListener _instance;
    
    private KeyCode _interactionKey = KeyCode.E;


    public Action InteractionKeyPressed;

    public static InputListener Instance { 
        get { return _instance; }
        private set { _instance = value; }
    }

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(_interactionKey)) {
            InteractionKeyPressed?.Invoke();
        }
    }


    public void OnInterationButtonPressed() {
        InteractionKeyPressed?.Invoke();
    }

}
