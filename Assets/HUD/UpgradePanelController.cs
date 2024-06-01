using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradePanelController : MonoBehaviour
{
    public static UpgradePanelController instance;
    public GameObject upgradePanel;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}
