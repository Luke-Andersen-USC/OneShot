using UnityEngine;
using TMPro;
using System.Collections;

public class UIBlast : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("Scale Slam")]
    [SerializeField] private float startScale = 0.2f;
    [SerializeField] private float overshootScale = 1.2f;
    [SerializeField] private float slamDuration = 0.15f;
    [SerializeField] private float settleDuration = 0.08f;

    [Header("Optional Effects")]
    [SerializeField] private bool shake = true;
    [SerializeField] private float shakeAmount = 8f;
    [SerializeField] private float shakeDuration = 0.1f;

    [SerializeField] private bool fadeOut = false;
    [SerializeField] private float fadeDelay = 0.5f;
    [SerializeField] private float fadeDuration = 0.25f;

    private AudioSource audioSource;
    private Vector3 originalPos;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (!text)
            text = GetComponent<TextMeshProUGUI>();

        originalPos = transform.localPosition;
    }

    public void Play()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(BlastRoutine());
    }

    private IEnumerator BlastRoutine()
    {
        transform.localScale = Vector3.one * startScale;

        audioSource.Play();
        
        // Slam in
        yield return ScaleTo(Vector3.one * overshootScale, slamDuration);

        // Settle
        yield return ScaleTo(Vector3.one, settleDuration);
        
        // Shake
        if (shake)
            yield return Shake();

        // Fade out
        if (fadeOut)
        {
            yield return new WaitForSeconds(fadeDelay);
            yield return FadeOut();
        }
    }

    private IEnumerator ScaleTo(Vector3 target, float time)
    {
        Vector3 start = transform.localScale;
        float t = 0f;

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(start, target, t / time);
            yield return null;
        }

        transform.localScale = target;
    }

    private IEnumerator Shake()
    {
        float t = 0f;

        while (t < shakeDuration)
        {
            t += Time.unscaledDeltaTime;
            Vector2 offset = Random.insideUnitCircle * shakeAmount;
            transform.localPosition = originalPos + (Vector3)offset;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        Color startColor = text.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            text.color = new Color(startColor.r, startColor.g, startColor.b, a);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
