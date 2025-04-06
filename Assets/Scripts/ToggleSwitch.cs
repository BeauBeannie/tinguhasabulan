using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{

    public AudioSource bgMusic; //bg music audiosource
    public AudioSource sfxSource; //sfx audio source
    public GameObject switchOn, switchOff;
    private bool musicMuted = false;
    private bool sfxMuted = false;

    public void OnChangeValue()
    {
        bool onoffSwitch = gameObject.GetComponent<Toggle>().isOn;
        if (onoffSwitch)
        {
            switchOn.SetActive(true);
            switchOff.SetActive(false);
        }
        if (!onoffSwitch)
        {
            switchOn.SetActive(false);
            switchOff.SetActive(true);

        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicMuted"))
        {
            PlayerPrefs.SetInt("musicMuted", 0);
        }

        if (!PlayerPrefs.HasKey("sfxMuted"))
        {
            PlayerPrefs.SetInt("sfxMuted", 0);
        }

        Load();
        ApplySettings();
    }

    public void OnMusicToggle()
    {
        musicMuted = !musicMuted;
        bgMusic.mute = musicMuted;
        Save();
    }

    public void OnSfxToggle()
    {
        sfxMuted = !sfxMuted; // Toggle SFX state
        if (sfxSource != null)
        {
            sfxSource.mute = sfxMuted; // Mute or unmute sfx
        }
        Save(); // Save preferences
    }

    /*public void OnButtonPress()
    {
        if(musicMuted == false)
        {
            musicMuted = true;
            AudioListener.pause = true;
        }
        else
        {
            musicMuted = false;
            AudioListener.pause = false;
        }

        Save();
    }*/

    //this is for the player's pref (PlayerPref)

    private void Load()
    {
        musicMuted = PlayerPrefs.GetInt("musicMuted") == 1;
        sfxMuted = PlayerPrefs.GetInt("sfxMuted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("musicMuted", musicMuted ? 1 : 0);
        PlayerPrefs.SetInt("sfxMuted", sfxMuted ? 1 : 0);
    }

    private void ApplySettings()
    {
        // Apply music and SFX mute settings
        if (bgMusic != null)
        {
            bgMusic.mute = musicMuted;
        }
        if (sfxSource != null)
        {
            sfxSource.mute = sfxMuted;
        }
    }


}

