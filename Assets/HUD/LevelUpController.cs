using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour
{
    [Header("Level Up Bar")]
    private float levelUp;
    private float lerpTimer;
    public float maxLevelUpBar = 15f;
    public float chipSpeed = 2f;
    public Image frontLevelUpBar;



    // Start is called before the first frame update
    void Start()
    {
        levelUp = 0;

    }

    // Update is called once per frame
    void Update()
    {
        levelUp = Mathf.Clamp(levelUp, 0, maxLevelUpBar);
        UpdateLevelUpUI();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddExperience(1);
        }

        if (levelUp >= maxLevelUpBar + 1)
        {
            frontLevelUpBar.fillAmount = 0;
            levelUp = 1;
            UpgradePanelController.instance.OpenUpgradePanel();


        }

    }

    public void UpdateLevelUpUI()
    {
        Debug.Log(levelUp);
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
