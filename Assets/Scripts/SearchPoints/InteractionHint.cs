using UnityEngine;
using UnityEngine.UI;

public class InteractionHint : MonoBehaviour
{
    [SerializeField] private GameObject _hintObject;
    [SerializeField] private Image _areaObject;
    [SerializeField] private Animation _emptyHint;


    public void EnableInteractionKeyHint(bool enable) { 
        _hintObject.SetActive(enable);
    }

    public void EnableInteractionAreaHint(bool enable)
    {
        _areaObject.gameObject.SetActive(enable);
        _areaObject.fillAmount = 0;
    }

    public void SetProgress(float progress)
    {
        _areaObject.fillAmount = progress;
    }


    public void ShowEmptyHint() {
        if (!_emptyHint.isActiveAndEnabled) {
            _emptyHint.gameObject.SetActive(true);
        }
        _emptyHint.Play();
    }

}
