using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SFXType { ButtonClick, PageFlip, ChapterFlip }


public class sfxButtonBinder : MonoBehaviour
{
    public SFXType sfxType;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null && sfxManager.instance != null)
        {
            button.onClick.AddListener(PlaySFX);
        }
    }

    void PlaySFX()
    {
        switch (sfxType)
        {
            case SFXType.ButtonClick:
                sfxManager.instance.PlayButtonClick();
                break;
            case SFXType.PageFlip:
                sfxManager.instance.PlayPageFlipClick();
                break;
            case SFXType.ChapterFlip:
                sfxManager.instance.PlayChapterFlipClick();
                break;
        }
    }
}
