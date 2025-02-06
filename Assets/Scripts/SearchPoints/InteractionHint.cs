using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    [SerializeField] private GameObject _hintObject;
    [SerializeField] private Animation _emptyHint;

    public void EnableInteractionHint(bool enable) { 
        _hintObject.SetActive(enable);
    }


    public void ShowEmptyHint() {
        if (!_emptyHint.isActiveAndEnabled) {
            Debug.Log("activating");
            _emptyHint.gameObject.SetActive(true);
        }
        _emptyHint.Play();
    }


}
