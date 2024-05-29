using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonBehavior : MonoBehaviour
{   
    public string _newGameLevel;
    public GameObject _panelStart;
    public GameObject _panelQuit;
    

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void PanelOnStart()
    {
        _panelStart.SetActive(true);   
    }

    public void PanelOffStart()
    {
        _panelStart.SetActive(false);   
    }

    public void PanelOnQuit()
    {
        _panelQuit.SetActive(true);   
    }

    public void PanelOffQuit()
    {
        _panelQuit.SetActive(false);   
    }		

    public void ExitButton()
    {
        Application.Quit();
    }
    
}
