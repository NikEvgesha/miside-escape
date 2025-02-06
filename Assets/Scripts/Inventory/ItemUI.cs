using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _img;

    public void Init(ItemData data) {
        _img.sprite = data.Img;
    }

}
