using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OilEffect : MonoBehaviour
{
    public Image oilImage;
    public float maxAlpha = 1f;
    public float fadeInTime = 0.15f;
    public float fadeOutTime = 0.3f;

    private void Awake()
    {
        if (oilImage == null) oilImage = GetComponent<Image>();
        Color c = oilImage.color;
        c.a = 0f;
        oilImage.color = c;
        oilImage.raycastTarget = false;
    }

    //Runs the effect for `duration` seconds (including fades)
    public void StartEffect(float duration)
    {
        StartCoroutine(EffectCoroutine(duration));
    }

    private IEnumerator EffectCoroutine(float duration)
    {
        // Fade in
        yield return Fade(0f, maxAlpha, fadeInTime);

        // Wait for the middle portion
        float wait = Mathf.Max(0f, duration - fadeInTime - fadeOutTime);
        yield return new WaitForSeconds(wait);

        // Fade out
        yield return Fade(maxAlpha, 0f, fadeOutTime);

        Destroy(gameObject);
    }

    private IEnumerator Fade(float from, float to, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(from, to, t / time);
            Color c = oilImage.color;
            c.a = a;
            oilImage.color = c;
            yield return null;
        }
        Color cFinal = oilImage.color;
        cFinal.a = to;
        oilImage.color = cFinal;
    }
}
