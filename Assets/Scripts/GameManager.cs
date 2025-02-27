using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int selectedLevel = 1; // Default Level 1
    private const string UNLOCKED_LEVEL_KEY = "UnlockedLevel"; // PlayerPrefs key

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1); // Load last selected level
    }

    public void SetSelectedLevel(int level)
    {
        selectedLevel = level;
        PlayerPrefs.SetInt("SelectedLevel", level); // Save current level
        PlayerPrefs.Save();
    }

    public void UnlockNextLevel()
    {
        int highestUnlockedLevel = PlayerPrefs.GetInt(UNLOCKED_LEVEL_KEY, 1);
        if (selectedLevel >= highestUnlockedLevel)
        {
            PlayerPrefs.SetInt(UNLOCKED_LEVEL_KEY, selectedLevel + 1);
            PlayerPrefs.Save(); // Save progress
        }
    }

    public bool IsLevelUnlocked(int level)
    {
        return level <= PlayerPrefs.GetInt(UNLOCKED_LEVEL_KEY, 1);
    }
}
