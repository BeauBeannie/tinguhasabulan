using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager instance { get; private set; }

    [SerializeField] private AudioSource buttonClick;

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
}