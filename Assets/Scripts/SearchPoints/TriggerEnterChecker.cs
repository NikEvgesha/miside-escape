using System;
using UnityEngine;

public class TriggerEnterChecker : MonoBehaviour
{
    public Action<bool> OnTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrigger?.Invoke(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrigger?.Invoke(false);
    }
}
