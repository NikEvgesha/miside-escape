using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public FootstepSurfaceData footstepData;
    public LayerMask groundLayer; // Выбери слой для земли
    public float raycastHeight = 1f; // Высота, с которой начинается луч

    public void PlayFootstep()
    {
        if (audioSource == null || footstepData == null) return;

        Vector3 rayStart = transform.position + Vector3.up * raycastHeight; // Поднимаем точку старта
        Ray ray = new Ray(rayStart, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f, groundLayer)) // 2f - запас на высоту персонажа
        {
            FootstepSurface surface = hit.collider.GetComponent<FootstepSurface>();
            SurfaceType type = surface != null ? surface.surfaceType : SurfaceType.Wood;

            AudioClip clip = footstepData.GetRandomStep(type);
            if (clip != null) audioSource.PlayOneShot(clip);
        }
    }
}
