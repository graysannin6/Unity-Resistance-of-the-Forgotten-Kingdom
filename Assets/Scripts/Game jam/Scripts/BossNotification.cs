using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossNotification : MonoBehaviour
{
    public Text bossSpawnText;
    public float displayDuration = 3f;
    public float fadeDuration = 0.5f;
    public Vector3 scaleUpSize = new Vector3(1.5f, 1.5f, 1.5f);
    public float wiggleAmplitude = 5f;
    public int wiggleVibrato = 10;
    public float wiggleRandomness = 90f;
    public bool wiggleSnapping = false;
    public bool wiggleFadeOut = true;

    private void Start()
    {
        bossSpawnText.gameObject.SetActive(false);
    }

    public void ShowBossSpawnMessage()
    {
        bossSpawnText.gameObject.SetActive(true);
        StartCoroutine(ShowMessageCoroutine());
    }

    private IEnumerator ShowMessageCoroutine()
    {
        yield return StartCoroutine(FadeScaleWiggleText(1, scaleUpSize));

        yield return new WaitForSeconds(displayDuration);

        yield return StartCoroutine(FadeScaleWiggleText(0, Vector3.one));

        bossSpawnText.gameObject.SetActive(false);
    }

    private IEnumerator FadeScaleWiggleText(float targetAlpha, Vector3 targetScale)
    {
        bossSpawnText.DOKill();

        bossSpawnText.DOFade(targetAlpha, fadeDuration);

        bossSpawnText.transform.DOScale(targetScale, fadeDuration);

        StartCoroutine(ShakeText(fadeDuration));

        yield return new WaitForSeconds(fadeDuration);
    }

    private IEnumerator ShakeText(float duration)
    {
        RectTransform rectTransform = bossSpawnText.GetComponent<RectTransform>();
        Vector2 originalPosition = rectTransform.anchoredPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float offsetX = Random.Range(-wiggleAmplitude, wiggleAmplitude);
            float offsetY = Random.Range(-wiggleAmplitude, wiggleAmplitude);
            rectTransform.anchoredPosition = new Vector2(originalPosition.x + offsetX, originalPosition.y + offsetY);

            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }

    private void HideBossSpawnMessage()
    {
        bossSpawnText.gameObject.SetActive(false);
    }
}


