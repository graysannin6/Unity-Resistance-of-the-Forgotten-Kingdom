using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanelController : MonoBehaviour
{
    public GameObject _gamePlayPanel;
    public GameObject _gamePlayUpgradePanel;
    private enum PanelState { GamePlay, Upgrade, None }
    private PanelState currentState;

    void Start()
    {
        ShowGamePlayPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (currentState)
            {
                case PanelState.GamePlay:
                    HideGamePlayPanel();
                    ShowGamePlayUpgradePanel();
                    break;
                case PanelState.Upgrade:
                    HideGamePlayUpgradePanel();
                    UnpauseGame();
                    break;
                case PanelState.None:
                    ShowGamePlayPanel();
                    break;
            }
        }
    }

    public void ShowGamePlayPanel()
    {
        _gamePlayPanel.SetActive(true);
        _gamePlayUpgradePanel.SetActive(false);
        Time.timeScale = 0;
        currentState = PanelState.GamePlay;
    }

    public void HideGamePlayPanel()
    {
        _gamePlayPanel.SetActive(false);
    }

    public void ShowGamePlayUpgradePanel()
    {
        _gamePlayUpgradePanel.SetActive(true);
        currentState = PanelState.Upgrade;
    }

    public void HideGamePlayUpgradePanel()
    {
        _gamePlayUpgradePanel.SetActive(false);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        currentState = PanelState.None;
    }
}
