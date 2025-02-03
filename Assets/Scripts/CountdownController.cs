using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;
    public GameObject controllers;
    public GameObject pauseButton;
    public GameObject timerText; //UI timer
    public Timer gameTimer;

    private void Start()
    {
        // Hide controllers and pause button initially
        if (controllers != null && pauseButton != null)
        {
            pauseButton.SetActive(false);
            controllers.SetActive(false);
            timerText.SetActive(false);
        }

        //Start the countdown coroutine
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            //remove the timer display when done
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "COLLECT!";

        //GameController.instance.BeginGame();

        //remove the "COLLECT!" display
        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

        if(controllers != null && pauseButton != null)  
        {
            controllers.SetActive(true);
            pauseButton.SetActive(true);
            timerText.SetActive(true);
        }

        //Start the timer only after the countdown finish
        
        if(gameTimer != null)
        {
            gameTimer.StartTimer();
        }
        
    }
}
