using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGameplayCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject inGamePauseCanvas;
    [SerializeField] private GameObject tutorialBoard;



    public void Awake()
    {
        //Disable lobby canvas
        if (lobbyCanvas != null)
        {
            lobbyCanvas.SetActive(true);
        }

        //Enable the gameplay canvas
        if (gameplayCanvas != null)
        {
            gameplayCanvas.SetActive(false);
        }

        //Disable In-Game Menu Canvas
        if (inGamePauseCanvas != null)
        {
            inGamePauseCanvas.SetActive(false);
        }

        if (tutorialBoard != null)
        {
            tutorialBoard.SetActive(false);
        }
    }


    public void StartGame()
    {
        //Disable lobby canvas
        if (lobbyCanvas != null)
        {
            lobbyCanvas.SetActive(false);
        }

        //Enable the gameplay canvas
        if (gameplayCanvas != null)
        {
            gameplayCanvas.SetActive(true);
        }

        //Disable In-Game Menu Canvas
        if (inGamePauseCanvas != null)
        {
            inGamePauseCanvas.SetActive(false);
        }

        if (tutorialBoard != null)
        {
            tutorialBoard.SetActive(false);
        }
    }

    public void ShowTutorial()
    {
        // Show tutorial board, hide others
        if (lobbyCanvas != null)
        {
            lobbyCanvas.SetActive(false);
        }

        if (gameplayCanvas != null)
        {
            gameplayCanvas.SetActive(false);
        }

        if (inGamePauseCanvas != null)
        {
            inGamePauseCanvas.SetActive(false);
        }

        if (tutorialBoard != null)
        {
            tutorialBoard.SetActive(true);
        }
    }

    public void CloseTutorial()
    {
        if (tutorialBoard != null)
        {
            tutorialBoard.SetActive(false);
        }

        if (lobbyCanvas != null)
        {
            lobbyCanvas.SetActive(true);
        }
    }
}
