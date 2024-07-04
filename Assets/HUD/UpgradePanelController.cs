using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradePanelController : MonoBehaviour
{
    public static UpgradePanelController instance;
    public GameObject upgradePanel;
    private PlayerMovement player;

    public TMP_Text counterDamageLevelText;
    public TMP_Text counterAbilityHasteLevelText;
    public TMP_Text counterMoveSpeedLevelText;
    public TMP_Text counterEnemiesKilledText;

    public  int counterDamageLevel = 0;
    public  int counterAbilityHasteLevel = 0;
    public  int counterMoveSpeedLevel = 0;
    public int counterEnemiesKilled = 0;

    [SerializeField] private List<WeaponInfo> weaponInfos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            player = FindObjectOfType<PlayerMovement>();

            LoadCounterValues();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCounterValues()
    {
        counterDamageLevel = PlayerPrefs.GetInt("CounterDamageLevel", 0);
        counterAbilityHasteLevel = PlayerPrefs.GetInt("CounterAbilityHasteLevel", 0);
        counterMoveSpeedLevel = PlayerPrefs.GetInt("CounterMoveSpeedLevel", 0);
        counterEnemiesKilled = PlayerPrefs.GetInt("CounterEnemiesKilled", 0);

        counterDamageLevelText.text = counterDamageLevel.ToString();
        counterAbilityHasteLevelText.text = counterAbilityHasteLevel.ToString();
        counterMoveSpeedLevelText.text = counterMoveSpeedLevel.ToString();
        counterEnemiesKilledText.text = counterEnemiesKilled.ToString();
    }

    private void SaveCounterValues()
    {
        PlayerPrefs.SetInt("CounterDamageLevel", counterDamageLevel);
        PlayerPrefs.SetInt("CounterAbilityHasteLevel", counterAbilityHasteLevel);
        PlayerPrefs.SetInt("CounterMoveSpeedLevel", counterMoveSpeedLevel);

        PlayerPrefs.Save();
    }

    public void OpenUpgradePanel()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        Time.timeScale = 1;
        upgradePanel.SetActive(false);
    }

    public void UpgradeDamage()
    {
        foreach (WeaponInfo weaponInfo in weaponInfos)
        {
            Debug.Log("Before upgrade: " + weaponInfo.weaponDamage);
            float newDamage = weaponInfo.weaponDamage + (weaponInfo.weaponDamage * 0.2f);
            weaponInfo.weaponDamage = newDamage;
            Debug.Log("After upgrade: " + weaponInfo.weaponDamage);
        }
        ActiveWeapon.Instance?.ApplyUpgrade();
        CloseUpgradePanel();
        counterDamageLevel++;
        counterDamageLevelText.text = counterDamageLevel.ToString();
        SaveCounterValues();
    }

    public void UpgradeAbilityHaste()
    {
        foreach (WeaponInfo weaponInfo in weaponInfos)
        {
            weaponInfo.weaponCooldown -= weaponInfo.weaponCooldown * 0.2f;
        }
        ActiveWeapon.Instance?.ApplyUpgrade();
        CloseUpgradePanel();
        counterAbilityHasteLevel++;
        counterAbilityHasteLevelText.text = counterAbilityHasteLevel.ToString();
        SaveCounterValues();
    }

    public void UpgradeMoveSpeed()
    {
        float newSpeed = player.GetMoveSpeed() * 1.1f;
        player.SetMoveSpeed(newSpeed);
        Debug.Log("New speed: " + player.GetMoveSpeed());
        CloseUpgradePanel();
        counterMoveSpeedLevel++;
        counterMoveSpeedLevelText.text = counterMoveSpeedLevel.ToString();
        SaveCounterValues();
    }

    public void SetCounterDamageLevel(int value)
    {
        counterDamageLevel = value;
        counterDamageLevelText.text = counterDamageLevel.ToString();
        SaveCounterValues();
    }

    public void SetCounterAbilityHasteLevel(int value)
    {
        counterAbilityHasteLevel = value;
        counterAbilityHasteLevelText.text = counterAbilityHasteLevel.ToString();
        SaveCounterValues();
    }

    public void SetCounterMoveSpeedLevel(int value)
    {
        counterMoveSpeedLevel = value;
        counterMoveSpeedLevelText.text = counterMoveSpeedLevel.ToString();
        SaveCounterValues();
    }

    public void SetCounterEnemiesKilled(int value)
    {
        counterEnemiesKilled = value;
        counterEnemiesKilledText.text = counterEnemiesKilled.ToString();
        SaveCounterValues();
    }

    public void AddEnemiesKilled(int value)
    {
        counterEnemiesKilled += value;
        counterEnemiesKilledText.text = counterEnemiesKilled.ToString();
        SaveCounterValues();
    }
}

