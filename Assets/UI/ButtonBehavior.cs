using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ButtonBehavior : MonoBehaviour
{   
    [Header("Volume Setting")]
    [SerializeField] private Slider _volumeSlider = null;
    [SerializeField] private TMP_Text _volumeTextValue = null;
    [SerializeField] private float _defaultVolume = 0.5f; 
    
    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("New Game Level")]
    public string _newGameLevel;
    
    [Header("Panels")]
    public GameObject _panelStart;
    public GameObject _panelQuit;
    public GameObject _panelSettings;
    public GameObject _panelAudioSettings;
    public GameObject _panelScreenSettings;
    public GameObject _panelCredits;
    
    // ESSENTIAL FUNCTION FOR LOAD THE GAME AFTER PRESS START
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    // REFACTORING FUNCTION FOR PANELS
    public void PanelOnStart()
    {
        _panelStart.SetActive(true);   
    }

    public void PanelOffStart()
    {
        _panelStart.SetActive(false);   
    }

    public void PanelOnSettings()
    {
        _panelSettings.SetActive(true);   
    }

    public void PanelOffSettings()
    {
        _panelSettings.SetActive(false);   
    }

    public void PanelOnAudioSettings()
    {
        _panelAudioSettings.SetActive(true);   
    }

    public void PanelOffAudioSettings()
    {
        _panelAudioSettings.SetActive(false);   
    }

    public void PanelOnScreenSettings()
    {
        _panelScreenSettings.SetActive(true);   
    }

    public void PanelOffScreenSettings()
    {
        _panelScreenSettings.SetActive(false);   
    }

    public void PanelOnCredits()
    {
        _panelCredits.SetActive(true);   
    }

    public void PanelOffCredits()
    {
        _panelCredits.SetActive(false);   
    }

    public void PanelOnQuit()
    {
        _panelQuit.SetActive(true);   
    }

    public void PanelOffQuit()
    {
        _panelQuit.SetActive(false);   
    }

    // ESSENTIAL FONCTION FOR AUDIO CONFIGURATION
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        _volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetVolume(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = _defaultVolume;
            _volumeSlider.value = _defaultVolume;
            _volumeTextValue.text = _defaultVolume.ToString("0.0");
        }
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }

    // ESSENTIAL FUNCTION FOR QUIT THE GAME AFTER PRESS QUIT
    public void ExitButton()
    {
        Application.Quit();
    }
    
}
