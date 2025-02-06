using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite _img;
    [SerializeField] private KeyItemType _type;

    public Sprite Img { get { return _img; }}
    public KeyItemType Type { get { return _type; } }
}
