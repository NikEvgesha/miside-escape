using UnityEngine;

public class CatchChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            GameManager.Instance.OnGameLose();
        }
    }
}
