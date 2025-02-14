using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoonManager : MonoBehaviour
{
    public static MoonManager instance;
    
    [SerializeField] public TextMeshProUGUI moonText; // UI text for collected moons
    [SerializeField] public int totalMoons; // Total moons in the current level
    [SerializeField] private int collectedMoons = 0; // How many moons collected

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
        Debug.Log("Level Complete!");  
        // Trigger next level, cutscene, or reward system here
    }

}
