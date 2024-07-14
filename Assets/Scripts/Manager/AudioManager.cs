using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);

        // Check if volume preferences exist, otherwise set default values
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 0.5f);
        }

        // Assign initial volumes
        ChangeMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        ChangeSoundVolume(PlayerPrefs.GetFloat("soundVolume"));
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        soundSource.volume = _change;
        PlayerPrefs.SetFloat("soundVolume", _change);
    }

    public void ChangeMusicVolume(float _change)
    {
        musicSource.volume = _change;
        PlayerPrefs.SetFloat("musicVolume", _change);
    }

    // Method to be called by the slider to set music volume
    public void SetMusicVolumeFromSlider(Slider slider)
    {
        ChangeMusicVolume(slider.value);
    }

    // Method to be called by the slider to set sound (SFX) volume
    public void SetSoundVolumeFromSlider(Slider slider)
    {
        ChangeSoundVolume(slider.value);
    }
}