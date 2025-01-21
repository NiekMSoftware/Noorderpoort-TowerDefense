using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Volume Sliders")]
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider enemyVolumeSlider;

    private void Start()
    {
        //If the player has changed their settings before
        if (PlayerPrefs.HasKey("mainVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMainVolume();
        }
    }

    public void SetMainVolume()
    {
        float volume = mainVolumeSlider.value;
        audioMixer.SetFloat("Main Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("mainVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxVolumeSlider.value;
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetEnemyVolume() {
        float volume = enemyVolumeSlider.value;
        this.audioMixer.SetFloat("Enemy Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("enemyVolume", volume);
    }

    /// <summary>
    /// Loads all the volume settings
    /// </summary>
    private void LoadVolume()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
        SetMainVolume();

        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMainVolume();

        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume();

        enemyVolumeSlider.value = PlayerPrefs.GetFloat("enemyVolume");
        SetEnemyVolume();
    }
}
