using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public FootstepSurfaceData footstepData;
    public LayerMask groundLayer; // ������ ���� ��� �����
    public float raycastHeight = 1f; // ������, � ������� ���������� ���

    public void PlayFootstep()
    {
        if (audioSource == null || footstepData == null) return;

        Vector3 rayStart = transform.position + Vector3.up * raycastHeight; // ��������� ����� ������
        Ray ray = new Ray(rayStart, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f, groundLayer)) // 2f - ����� �� ������ ���������
        {
            FootstepSurface surface = hit.collider.GetComponent<FootstepSurface>();
            SurfaceType type = surface != null ? surface.surfaceType : SurfaceType.Wood;

            AudioClip clip = footstepData.GetRandomStep(type);
            if (clip != null) audioSource.PlayOneShot(clip);
        }
    }
}
