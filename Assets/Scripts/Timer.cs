using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool isRunning = false; // Timer is initially stopped

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
    }
}

    // Call this method to start the timer
    public void StartTimer()
    {
        isRunning = true;
    }
}