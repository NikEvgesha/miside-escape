using UnityEngine;
using UnityEngine.UI;

public class AttackHint : MonoBehaviour
{
    [SerializeField] private Image _hintImg;
    [SerializeField] private Animator _animator;

/*    [SerializeField] private Color _prepareColor;
    [SerializeField] private Color _attackColor;*/

    private void Start()
    {
        EnemyAI.Instance.SawPrepare += OnSawPrepare;
        EnemyAI.Instance.AttackStart += OnAttackStart;
        EnemyAI.Instance.AttackEnd += OnAttackEnd;
    }

    private void OnDisable()
    {
        EnemyAI.Instance.SawPrepare -= OnSawPrepare;
        EnemyAI.Instance.AttackStart -= OnAttackStart;
        EnemyAI.Instance.AttackEnd -= OnAttackEnd;
    }

    private void OnSawPrepare() {
        //_hintImg.color = _prepareColor;
        _animator.SetTrigger("SawPrepare");
    }

    private void OnAttackStart()
    {
        //_hintImg.color = _attackColor;
        _animator.SetTrigger("AttackStart");
    }

    private void OnAttackEnd()
    {
        _animator.SetTrigger("AttackEnd");
    }
}
