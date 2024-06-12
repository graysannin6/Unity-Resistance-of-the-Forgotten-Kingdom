using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanelController : MonoBehaviour
{
    public GameObject _gamePlayPanel;
    public GameObject _gamePlayUpgradePanel;
    public bool isShowing = true;
    void Start()
    {
        ShowGamePlayPanel();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
{
    if (Input.GetKeyDown(KeyCode.F))
    {   
        if (isShowing)
        {
            HideGamePlayUpgradePanel();
            isShowing = false;
        }
        else
        {
            HideGamePlayPanel();
            Time.timeScale = 1;
            ShowGamePlayUpgradePanel();
            isShowing = true;
        }
    }
}

    public void ShowGamePlayPanel()
    {
        _gamePlayPanel.SetActive(true);
    }

    public void HideGamePlayPanel()
    {
        _gamePlayPanel.SetActive(false);
    }

    public void ShowGamePlayUpgradePanel()
    {
        _gamePlayUpgradePanel.SetActive(true);
    }

    public void HideGamePlayUpgradePanel()
    {
        _gamePlayUpgradePanel.SetActive(false);
    }
}
