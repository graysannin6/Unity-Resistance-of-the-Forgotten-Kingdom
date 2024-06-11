using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArrowNotification : MonoBehaviour
{
    public Transform bossTransform;
    public RectTransform arrowRectTransform;
    public float hideDistance = 5.0f;

    public void NavigateArrow()
    {
        Vector3 toBoss = bossTransform.position - Camera.main.transform.position;
        float distanceToBoss = toBoss.magnitude;

        // Check if within hide distance
        if (distanceToBoss < hideDistance)
        {
            arrowRectTransform.gameObject.SetActive(false);
        }
        else
        {
            arrowRectTransform.gameObject.SetActive(true);
            float angle = Mathf.Atan2(toBoss.z, toBoss.x) * Mathf.Rad2Deg;
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}
