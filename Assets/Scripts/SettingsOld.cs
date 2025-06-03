using System;
using UnityEngine;

public class SettingsOld : MonoBehaviour
{
    /*
     Music volume
     Sound Volume
     Language
     
     */

    // TODO: вынести в лобби, настройки должны быть всегда, настроить UI

    public static SettingsOld instance;
    public Action<float> ChangeMouseSensitivity;
    public Action<float> ChangeVolume;
    [SerializeField] private GameObject _UIWindow;

    private bool _isOpen;


    private void OnEnable()
    {
        _isOpen = false;
    }

    // отдельно музыка и звуки - только ползунки
    private SettingsOld()
    {
        instance = this;
    }
    public void Sensitivity(float sens)
    {
        ChangeMouseSensitivity?.Invoke(sens);
    }

    public void SoundVolume(float volume)
    {
        SoundManagerOld.Instance.SoundVolume = volume;
    }
    public void MusicVolume(float volume)
    {
        SoundManagerOld.Instance.MusicVolume = volume;
    }

    public void ToggleUIOpen()
    {
        _isOpen = !_isOpen;
        _UIWindow.SetActive(_isOpen);
    }
}
