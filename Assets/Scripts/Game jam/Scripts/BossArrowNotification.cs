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

            // Calculate the direction to the boss in world space
            Vector3 bossScreenPosition = Camera.main.WorldToScreenPoint(bossTransform.position);
            Vector3 arrowScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = bossScreenPosition - arrowScreenPosition;

            // Calculate the angle in 2D space
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}
