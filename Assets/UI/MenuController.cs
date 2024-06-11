using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private Slider _volumeSlider = null;
    [SerializeField] private TMP_Text _volumeTextValue = null;
    [SerializeField] private float _defaultVolume = 0.5f;

    [Header("Screen Settings")]
    [SerializeField] private Slider _brightnessSlider = null;
    [SerializeField] private TMP_Text _brightnessTextValue = null;
    [SerializeField] private float _defaultBrightness = 0.5f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;

    [Header("Confirmation")]
    [SerializeField] private GameObject _confirmationPrompt = null;

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

    public void Reset(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = _defaultVolume;
            _volumeSlider.value = _defaultVolume;
            _volumeTextValue.text = _defaultVolume.ToString("0.0");
        }

        if (MenuType == "Screen")
        {
            _brightnessSlider.value = _defaultBrightness;
            _brightnessTextValue.text = _defaultBrightness.ToString("0.0");

            _qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            _fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            _resolutionDropdown.value = _resolutions.Length;
            GraphicsApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        _confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        _confirmationPrompt.SetActive(false);
    }

    // SCREEN SETTINGS FUNCTIONS

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        _brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", _isFullScreen ? 1 : 0);
        Screen.fullScreen = _isFullScreen;
        StartCoroutine(ConfirmationBox());
    }

    // RESOLUTION SETTINGS FUNCTIONS
    private void Start()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == 1920 && _resolutions[i].height == 1080)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        // Set initial settings
        _qualityDropdown.value = 2; // Medium quality index, change if your quality settings differ
        QualitySettings.SetQualityLevel(2);

        _fullScreenToggle.isOn = true;
        Screen.fullScreen = true;

        Screen.SetResolution(1920, 1080, false);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // ESSENTIAL FUNCTION FOR QUIT THE GAME AFTER PRESS QUIT
    public void ExitButton()
    {
        Application.Quit();
    }

}
