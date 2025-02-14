using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGameplayCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject inGamePauseCanvas;


    public void Awake()
    {
        //Disable lobby canvas
        if (lobbyCanvas != null)
        {
            lobbyCanvas.SetActive(true);
        }

        //Enable the gameplay canvas
        if(gameplayCanvas != null)
        {
            gameplayCanvas.SetActive(false);
        }

        //Disable In-Game Menu Canvas
        if(inGamePauseCanvas != null)
        {
            inGamePauseCanvas.SetActive(false);
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
        if(gameplayCanvas != null)
        {
            gameplayCanvas.SetActive(true);
        }

        //Disable In-Game Menu Canvas
        if(inGamePauseCanvas != null)
        {
            inGamePauseCanvas.SetActive(false);
        }
    }
}
