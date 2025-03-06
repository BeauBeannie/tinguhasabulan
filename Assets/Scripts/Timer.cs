using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool isRunning = false; // Timer is initially stopped
    private bool isFlashing = false; // To prevent multiple coroutines running
    private Color defaultColor;


    void Start()
    {
        defaultColor = timerText.color; // Save the original text color
    }

    void Update()
    {
        if (isRunning)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isRunning = false;
                Debug.Log("Time's up! Checking if moons were collected...");

                if (MoonManager.instance != null)
                {
                    MoonManager.instance.GameOver(); // Calls Game Over when time runs out
                }
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            //starts flashing when 10 secs or less remains
            if (remainingTime <= 10 && !isFlashing)
            {
                StartCoroutine(FlashText());
            }
        }
    }

    // Call this method to start the timer
    public void StartTimer()
    {
        isRunning = true;
    }

    IEnumerator FlashText()
    {
        isFlashing = true;
        while (remainingTime > 0 && remainingTime <= 10)
        {
            timerText.color = Color.red; // Change to red
            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

            timerText.color = defaultColor; // Revert to original color
            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        }

        timerText.color = defaultColor; // Ensure it resets after the timer stops
        isFlashing = false; // Allow it to restart if needed
    }
}