using UnityEngine;

public class CatchChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.GameInProgress && other.gameObject.GetComponent<EnemyAI>())
        {
            GameManager.Instance.OnGameLose();
        }
    }
}
