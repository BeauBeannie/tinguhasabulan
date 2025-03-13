using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance { get; private set; }

    public AudioSource backgroundMusic;  // Assign in Inspector
    public AudioSource oceanWaves;       // Assign in Inspector

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float volume)
    {
        if (backgroundMusic != null)
            backgroundMusic.volume = volume;

        if (oceanWaves != null)
            oceanWaves.volume = volume;
    }
}
