using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeControl : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider; // UI Slider for SFX

    void Start()
    {
        // Load saved volume or default to 1
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }

        Load();
        ApplyVolume();
    }

    public void ChangeVolume()
    {
        ApplyVolume();
        Save();
    }

    private void ApplyVolume()
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.SetSFXVolume(sfxSlider.value);
        }
    }

    private void Load()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }
}
