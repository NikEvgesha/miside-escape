using UnityEngine;
using UnityEngine.UI;

public class DoorUI : MonoBehaviour
{
    [SerializeField] private Image _requiredItemImage;
    [SerializeField] private Animation _requiredItemAnimation;

    public void SetKeyImage(Sprite img) {
        _requiredItemImage.sprite = img;
    }

    public void ToggleImage(bool enable)
    {
        _requiredItemImage.gameObject.SetActive(enable);
    }

    public void ShowRequiredItemHint() {
        Debug.Log("No Key!");
        _requiredItemAnimation.Play();
    }

}
