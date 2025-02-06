using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHint : MonoBehaviour
{
    [SerializeField] private GameObject _hintObject;
    [SerializeField] private Animation _emptyHint;
    [SerializeField] private Image _reqiredItemImage;

    public void SetRequiredItemImage(Sprite sprite) {
        _reqiredItemImage.sprite = sprite;
    }

    public void EnableInteractionHint(bool enable) { 
        _hintObject.SetActive(enable);
    }


    public void ShowEmptyHint() {
        if (!_emptyHint.isActiveAndEnabled) {
            _emptyHint.gameObject.SetActive(true);
        }
        _emptyHint.Play();
    }

    public void ToggleImageHint(bool enable) {
        _reqiredItemImage.gameObject.SetActive(enable);
    }
}
