using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHint : MonoBehaviour
{
    [SerializeField] private GameObject _hintObject;
    [SerializeField] private Animation _emptyHint;


    public void EnableInteractionHint(bool enable) { 
        _hintObject.SetActive(enable);
    }


    public void ShowEmptyHint() {
        if (!_emptyHint.isActiveAndEnabled) {
            _emptyHint.gameObject.SetActive(true);
        }
        _emptyHint.Play();
    }

}
