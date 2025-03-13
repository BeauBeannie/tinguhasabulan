using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // UI Slider

    void Start()
    {
        // Load saved volume or default to 1
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
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
        if (BackgroundMusic.instance != null)
        {
            BackgroundMusic.instance.SetVolume(volumeSlider.value);
        }
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
}