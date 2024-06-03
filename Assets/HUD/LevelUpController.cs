 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : Singleton<LevelUpController>
{
    [Header("Level Up Bar")]
    private float levelUp;
    private float lerpTimer;
    public float maxLevelUpBar = 16f;
    public float chipSpeed = 2f;
    public Image frontLevelUpBar;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        levelUp = 0;

    }
    void Update()
    {
        levelUp = Mathf.Clamp(levelUp, 0, maxLevelUpBar);
        UpdateLevelUpUI();

        if (levelUp >= maxLevelUpBar)
        {
            frontLevelUpBar.fillAmount = 0;
            levelUp = 0;
            UpgradePanelController.instance.OpenUpgradePanel();
        }

    }

    public void UpdateLevelUpUI()
    {
        float fillFront = frontLevelUpBar.fillAmount;
        float hFraction = levelUp / maxLevelUpBar;

        if (fillFront < hFraction)
        {
            frontLevelUpBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontLevelUpBar.fillAmount = Mathf.Lerp(fillFront, hFraction, percentComplete);
        }
    }

    public void AddExperience(float experience)
    {
        levelUp += experience;
        lerpTimer = 0f;
    }

    public void RemoveExperience(float experience)
    {
        levelUp -= experience;
        lerpTimer = 0f;
    }
}
