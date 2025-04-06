using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; private set; }

    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource chapterFlip;
    [SerializeField] private AudioSource pageFlip;

    private void Awake()
    {
        // Ensure only one instance of SFXManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Keep across scenes

        LoadSFXVolume(); // Load volume settings on startup
    }

    private void OnEnable()
    {
        LoadSFXVolume(); // Ensure volume is reapplied when switching scenes
    }

    public void PlayButtonClick()
    {
        if (buttonClick != null) buttonClick.Play();
        else Debug.LogWarning("No AudioSource assigned to buttonClick in SFXManager.");
    }

    public void PlayChapterFlipClick()
    {
        if (chapterFlip != null) chapterFlip.Play();
        else Debug.LogWarning("No AudioSource assigned to chapterFlip in SFXManager.");
    }

    public void PlayPageFlipClick()
    {
        if (pageFlip != null) pageFlip.Play();
        else Debug.LogWarning("No AudioSource assigned to pageFlip in SFXManager.");
    }

    public void SetSFXVolume(float volume)
    {
        if (buttonClick != null) buttonClick.volume = volume;
        if (chapterFlip != null) chapterFlip.volume = volume;
        if (pageFlip != null) pageFlip.volume = volume;

        SaveSFXVolume(volume);
    }

    private void LoadSFXVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("sfxVolume", 1f); // Default to 1 if missing
        SetSFXVolume(savedVolume);
    }

    private void SaveSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }
}
