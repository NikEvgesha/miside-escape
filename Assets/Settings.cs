using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    /*
     Music volume
     Sound Volume
     Language
     
     */

    // TODO: вынести в лобби, настройки должны быть всегда, настроить UI

    public static Settings instance;
    public Action<float> ChangeMouseSensitivity;
    public Action<float> ChangeVolume;
    [SerializeField] private GameObject _UIWindow;

    private bool _isOpen;


    private void OnEnable()
    {
        _isOpen = false;
    }

    // отдельно музыка и звуки - только ползунки
    private Settings()
    {
        instance = this;
    }
    public void Sensitivity(float sens)
    {
        ChangeMouseSensitivity?.Invoke(sens);
    }

    public void SoundVolume(float volume)
    {
        SoundManager.Instance.SoundVolume = volume;
    }
    public void MusicVolume(float volume)
    {
        SoundManager.Instance.MusicVolume = volume;
    }

    public void ToggleUIOpen()
    {
        _isOpen = !_isOpen;
        _UIWindow.SetActive(_isOpen);
    }
}
