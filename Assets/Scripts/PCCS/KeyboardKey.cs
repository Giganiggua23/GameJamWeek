using UnityEngine;
using System.Collections;

public class KeyboardKey : MonoBehaviour
{
    public string key;
    public float pressDepth = 0.03f;
    public float pressDuration = 0.2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pressSound;
    public AudioClip releaseSound;

    private Vector3 originalPosition;
    private bool isAnimating = false;

    private void Start()
    {
        originalPosition = transform.localPosition;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PressButton()
    {
        if (isAnimating) return;
        StartCoroutine(PressAnimation());
    }

    private IEnumerator PressAnimation()
    {
        isAnimating = true;

        // Воспроизводим звук нажатия
        if (audioSource != null && pressSound != null)
        {
            audioSource.PlayOneShot(pressSound);
        }

        // Передаем нажатую клавишу контроллеру
        PcController controller = GetComponentInParent<PcController>();
        if (controller != null)
        {
            controller.PasswordEntry(key);
        }

        // Анимация нажатия
        Vector3 targetPosition = originalPosition + Vector3.down * pressDepth;
        float elapsed = 0f;

        while (elapsed < pressDuration / 2f)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsed / (pressDuration / 2f));
            yield return null;
        }

        // Ждем немного в нажатом состоянии
        yield return new WaitForSeconds(0.1f);

        // Анимация возврата
        elapsed = 0f;
        while (elapsed < pressDuration / 2f)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, elapsed / (pressDuration / 2f));
            yield return null;
        }

        // Воспроизводим звук отпускания
        if (audioSource != null && releaseSound != null)
        {
            audioSource.PlayOneShot(releaseSound);
        }

        transform.localPosition = originalPosition;
        isAnimating = false;
    }

    public void SendKey()
    {
        PressButton();
    }
}