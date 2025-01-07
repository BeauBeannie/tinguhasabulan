using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGameplayCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCanvas;
    [SerializeField] private GameObject gameplayCanvas;


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
    }
}
