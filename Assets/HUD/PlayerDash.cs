using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    public Image frontDashBar;
    public Image backDashBar;
    public float maxDash = 100f;
    public float dashRechargeDuration = 3f; 
    public float dashUseAmount = 100f; 

    private float dash;
    private bool isRecharging;

    private void Start()
    {
        dash = maxDash;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            UseDash(dashUseAmount);
        }

        if (frontDashBar.fillAmount == 0 && !isRecharging)
        {
            StartCoroutine(RestoreDash());
        }
    }

    private void UseDash(float amount)
    {
        dash -= amount;
        dash = Mathf.Clamp(dash, 0, maxDash);
        UpdateDashUI();
    }

    private void UpdateDashUI()
    {
        float fillFront = dash / maxDash;
        frontDashBar.fillAmount = fillFront;
        backDashBar.fillAmount = fillFront; 
    }

    private IEnumerator RestoreDash()
    {
        isRecharging = true;
        float elapsed = 0f;

        while (elapsed < dashRechargeDuration)
        {
            elapsed += Time.deltaTime;
            dash = Mathf.Lerp(0, maxDash, elapsed / dashRechargeDuration);
            UpdateDashUI();
            yield return null;
        }

        // Ensure the dash bar is fully filled
        dash = maxDash;
        UpdateDashUI();
        isRecharging = false;
    }
}
