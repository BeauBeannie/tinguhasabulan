using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButtonLS : MonoBehaviour
{
    public void LoadSelectedLevel()
    {
        if (GameManager.Instance == null) 
        {
            Debug.LogError("GameManager is not initialized!");
            return;
        }

        int levelToLoad = GameManager.Instance.selectedLevel;
        string sceneName = "Level " + levelToLoad; // Ensure the space matches your scene names

        Debug.Log("Loading Scene: " + sceneName); // Debugging output
        SceneManager.LoadScene(sceneName);
    }
}
