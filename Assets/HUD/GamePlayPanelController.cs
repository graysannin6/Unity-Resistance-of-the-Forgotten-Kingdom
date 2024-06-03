using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanelController : MonoBehaviour
{
    public GameObject _gamePlayPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            HideGamePlayPanel();
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
}
