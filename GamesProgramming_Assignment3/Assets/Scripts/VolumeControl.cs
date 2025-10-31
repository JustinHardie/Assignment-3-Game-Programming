using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("UI Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Load saved settings or use defaults
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);

        // Listen for slider changes
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= 0.0001f)
            mainMixer.SetFloat("MusicVolume", -80f); // mute
        else
            mainMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= 0.0001f)
            mainMixer.SetFloat("SFXVolume", -80f); // mute
        else
            mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

}
