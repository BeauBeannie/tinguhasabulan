using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked = false; // Level lock status
    public GameObject lockImage;
    public TextMeshProUGUI levelNumbers;
    public Button levelButton;
    public int levelIndex; // Assign this in the Inspector (1 for Level 1, 2 for Level 2, etc.)

    private void Start()
    {
        if (levelIndex == 1) // Ensure Level 1 is unlocked by default
        {
            unlocked = true;
            PlayerPrefs.SetInt($"Level{levelIndex}Unlocked", 1); // Ensure it's always saved
        }
        else
        {
            // Load unlock status from PlayerPrefs
            unlocked = PlayerPrefs.GetInt($"Level{levelIndex}Unlocked", 0) == 1;
        }

        
        UpdateLevelImage();
    }

    private void UpdateLevelImage()
    {
        if (!unlocked) // If locked
        {
            lockImage.SetActive(true);
            levelNumbers.gameObject.SetActive(false);
            levelButton.interactable = false; // Disable button
        }
        else // If unlocked
        {
            lockImage.SetActive(false);
            levelNumbers.gameObject.SetActive(true);
            levelButton.interactable = true; // Enable button
        }
    }

    public void SelectLevel()
    {
        if (unlocked)
        {
            GameManager.Instance.SetSelectedLevel(levelIndex);
        }
    }

    public void UnlockLevel()
    {
        unlocked = true;
        PlayerPrefs.SetInt($"Level{levelIndex}Unlocked", 1); // Save unlock state
        PlayerPrefs.Save();
        UpdateLevelImage();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); // Completely clears saved progress
        PlayerPrefs.SetInt("Level1Unlocked", 1); // Ensure Level 1 stays unlocked
        PlayerPrefs.Save();
        Debug.Log("Progress reset: Only Level 1 is unlocked.");
    }

}
