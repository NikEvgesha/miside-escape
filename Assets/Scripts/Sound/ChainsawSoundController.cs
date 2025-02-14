using UnityEngine;

public class ChainsawSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _startSound;
    [SerializeField] private AudioClip _fullPowerSound;
    [SerializeField] private AudioClip _idleSound;
    [SerializeField] private int _distanceWait = 200;
    [SerializeField] private int _distanceNormal = 20;

    private void Start()
    {
        // ”бедитесь, что AudioSource не воспроизводит звук при старте
        _audioSource.Stop();
    }

    public void PlayStartSound()
    {
        _audioSource.maxDistance = _distanceWait;
        _audioSource.loop = false;
        _audioSource.clip = _startSound;
        _audioSource.Play();
    }

    public void PlayFullPowerSound()
    {
        _audioSource.maxDistance = _distanceNormal;
        _audioSource.loop = true;
        _audioSource.clip = _fullPowerSound;
        _audioSource.Play();
    }

    public void PlayIdleSound()
    {
        _audioSource.maxDistance = _distanceNormal;
        _audioSource.loop = true;
        _audioSource.clip = _idleSound;
        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }
}
