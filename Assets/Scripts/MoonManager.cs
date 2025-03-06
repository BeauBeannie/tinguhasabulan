using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoonManager : MonoBehaviour
{
    public static MoonManager instance;
    
    [SerializeField] public TextMeshProUGUI moonText; // UI text for collected moons
    [SerializeField] public int totalMoons; // Total moons in the current level
    private int collectedMoons = 0; // How many moons collected

    private int currentLevel;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLevel = GameManager.Instance.selectedLevel; // Get current level from GameManager
        UpdateMoonUI(); // Set initial UI (e.g., 0/3)
    }

    public void SetTotalMoons(int moons)
    {
        totalMoons = moons;
        collectedMoons = 0; // Reset collected moons at start of each level
        UpdateMoonUI();
    }

    public void CollectMoon()
    {
        collectedMoons++;
        UpdateMoonUI();

        if (collectedMoons >= totalMoons)
        {
            LevelComplete();
        }
    }

    private void UpdateMoonUI()
    {
        if (moonText != null)
        {
            moonText.text = $"{collectedMoons}/{totalMoons}";
        }
    }

    private void LevelComplete()
    {
        if(isGameOver) return; //stops if game over
        isGameOver = true; //prevent multiple triggers

        int nextLevel = currentLevel + 1;

        if (nextLevel <= 6) // Ensure we don't go past Level 6
        {
            PlayerPrefs.SetInt($"Level{nextLevel}Unlocked", 1); // Unlock next level
            PlayerPrefs.Save();
        }

        StartCoroutine(LoadCutsceneWithDelay());
    }

    public void GameOver()
    {
        if(isGameOver) return; //Stop if already game over

        isGameOver = true;
        Debug.Log("Game Over! Failed to collect all moons.");
        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadCutsceneWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        Debug.Log("Level Complete!");  
        SceneManager.LoadScene("Cutscene"); // Load the cutscene
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
}
