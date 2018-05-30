using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject mainCanvas, namingCanvas, loadGameCanvas;
    void Start()
    {
        PlayerData.LoadStatData();
    }

    // showing naming canvas
    public void StartButton()
    {
        namingCanvas.SetActive(true);
		mainCanvas.SetActive(false);
    }

    // confirm naming and start game
    public void NamingConfirm(Text name)
    {
        if (name.text.Length <= 0)
            return;
        GameController.activatingPlayer = new PlayerData();
		GameController.activatingPlayer.PlayerName = name.text;
        GameController.LoadLevel(GameController.activatingPlayer.LatestGameLevel);
    }

    // back to main menu
    public void NamingCancel()
    {
        namingCanvas.SetActive(false);
		mainCanvas.SetActive(true);
    }

    // showing save/load game canvas
    public void LoadGameButton()
    {
        loadGameCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
