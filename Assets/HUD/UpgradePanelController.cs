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

    private int counterDamageLevel =0;
    private int counterAbilityHasteLevel =0;
    private int counterMoveSpeedLevel =0;

    [SerializeField] private List<WeaponInfo> weaponInfos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<PlayerMovement>();
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

    }

    public void UpgradeMoveSpeed()
    {
        float newSpeed = player.GetMoveSpeed() * 1.1f;
        player.SetMoveSpeed(newSpeed);
        Debug.Log("New speed: " + player.GetMoveSpeed());
        CloseUpgradePanel();
        counterMoveSpeedLevel++;
        counterMoveSpeedLevelText.text = counterMoveSpeedLevel.ToString();
        
    }
}

