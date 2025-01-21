using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager instance { get; private set; }

    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource chapterFlip;
    [SerializeField] private AudioSource pageFlip;

    private void Awake()
    {
        // Ensure only one instance of sfxManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Keep this across scenes
    }

    public void PlayButtonClick()
    {
        if (buttonClick != null)
        {
            buttonClick.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned to buttonClick in sfxManager.");
        }
    }

    public void PlayChapterFlipClick()
    {
        if (chapterFlip != null)
        {
            chapterFlip.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned to chapterFlip in sfxManager.");
        }
    }

    public void PlayPageFlipClick()
    {
        if (pageFlip != null)
        {
            pageFlip.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned to pageFlip in sfxManager.");
        }
    }
    
    
}