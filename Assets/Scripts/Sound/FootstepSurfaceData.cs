using UnityEngine;

[CreateAssetMenu(fileName = "FootstepData", menuName = "Audio/Footstep Data")]
public class FootstepSurfaceData : ScriptableObject
{
    public AudioClip[] woodSteps;
    public AudioClip[] stoneSteps;
    public AudioClip[] carpetSteps;

    public AudioClip GetRandomStep(SurfaceType type)
    {
        AudioClip[] clips = type switch
        {
            SurfaceType.Wood => woodSteps,
            SurfaceType.Stone => stoneSteps,
            SurfaceType.Carpet => carpetSteps,
            _ => null
        };

        return (clips != null && clips.Length > 0) ? clips[Random.Range(0, clips.Length)] : null;
    }
}

public enum SurfaceType { Wood, Stone, Carpet }
